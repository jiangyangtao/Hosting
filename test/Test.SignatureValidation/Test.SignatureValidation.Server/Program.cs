using Yangtao.Hosting.SignatureValidation.Core.Enums;
using Yangtao.Hosting.SignatureValidation.Server;

namespace Test.SignatureValidation.Server
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
                options.HmacShaAlgorithmType = HashAlgorithmType.HmacSha256;
                options.HmacShaSignatureFormatType = HmacShaSignatureFormatType.Base64;
                options.SecretKey = "3d37adf4f8a593811d8035c9a355bb25";
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseServerSignatureValidation();
            app.MapControllers();

            app.Run();
        }
    }
}