using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Grpc.Net.ClientFactory;

namespace Yangtao.Hosting.GrpcClient
{
    public static class GrpcClientFactoryOptionsExtensions
    {
        public static GrpcClientFactoryOptions SetConfig(this GrpcClientFactoryOptions options, GrpcClientOptions clientOptions)
        {
            return options.SetConfig(clientOptions);
        }

        public static GrpcClientOptions SetConfig(this GrpcClientFactoryOptions options, Action<GrpcClientOptions> optionAction)
        {
            var clientOptions = new GrpcClientOptions();
            optionAction(clientOptions);

            options.Address = new Uri(clientOptions.Endpoint);
            options.ChannelOptionsActions.Add((channelOptions) =>
            {
                if (clientOptions.AllowUnsafeCertificate)
                {
                    // 允许自签名证书
                    channelOptions.HttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                    };
                }

                if (clientOptions.AddRetry)
                {
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
                }
            });

            return clientOptions;
        }
    }
}
