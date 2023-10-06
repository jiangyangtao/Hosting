using Grpc.Core;
using Grpc.Net.Client.Configuration;
using RSAExtensions;
using Test.SignatureValidation.ClientGrpc.Provider;
using Yangtao.Hosting.SignatureValidation.Client;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Test.SignatureValidation.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHcmaShaSignatureValidation(options =>
            {
                options.HmacShaAlgorithmType = HashAlgorithmType.SHA256;
                options.HmacShaSignatureFormatType = HmacShaSignatureFormatType.Base64;
                options.SecretKey = "3d37adf4f8a593811d8035c9a355bb25";
            });
            builder.Services.AddEncryptionValidation(options =>
            {
                options.RSAEncryptionPaddingType = RSAEncryptionPaddingType.Pkcs1;
                options.RSAKeyType = RSAKeyType.Pkcs8;
                options.PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArlREt0S8iWue9mVjwjbAELLztEh3z6vbLyGL2hdVadmrHE1v3+7xMUvLn2HY+wE2hJNfNv6Ai5SuEZWlVg94n0tEqKcBs3tNskqIcpU684mt4INgtLl/c04oEkhEzFfcjv6QVMulPLAKEy13RnlsnWwob+sjEjonH+HcLMBwB8XW9EyhwFuAySjpAr6HVQ8lMJVeV1L45W0cO+PxEaFvvAoOwERpssBV3KY3dMi2USW6t8WcuY0qcLw4wdv+qCP9P0pzMbF98aJQUkZoL80GWtq/6HyD994ZRD9o/d1C3RqhQPs3mMByEqiu2X7JDi92/GphKZ9uQYMHx7an8PggfQIDAQAB";
            });
            builder.Services.AddGrpcClient<ClientSignatureValidationGrpcProvider.ClientSignatureValidationGrpcProviderClient>(options =>
            {
                options.Address = new Uri("https://localhost:7251");
                options.ChannelOptionsActions.Add((channelOptions) =>
                {
                    // 允许自签名证书
                    channelOptions.HttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                    var serviceConfig = new ServiceConfig();
                    serviceConfig.MethodConfigs.Add(new MethodConfig
                    {
                        Names = { MethodName.Default },
                        RetryPolicy = new RetryPolicy       // 重试策略
                        {
                            MaxAttempts = 5,
                            InitialBackoff = TimeSpan.FromSeconds(1),
                            MaxBackoff = TimeSpan.FromSeconds(5),
                            BackoffMultiplier = 1.5,
                            RetryableStatusCodes = { StatusCode.Unavailable }
                        }
                    });
                    channelOptions.ServiceConfig = serviceConfig;
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}