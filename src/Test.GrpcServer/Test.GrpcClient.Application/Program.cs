using Test.GrpcServer.Provider;
using Yangtao.Hosting.GrpcClient;
using Yangtao.Hosting.GrpcCore.Options;

namespace Test.GrpcClient.Application
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

            builder.Services.AddGrpcClientService<GrpcValidationProvider.GrpcValidationProviderClient>(options =>
            {
                options.Endpoint = "https://localhost:7294/";
                options.UseAuthenticationGrpcClientInterceptor = false;
                options.SignAuthenticationType = SignAuthenticationType.Aes;
                options.AesSignOptions = new AesSignOptions
                {
                    Iv = "e9c88e6b36c14c8e",
                    SecurityKey = "550cc5113bf44ec7ac9835df2fe49b32",
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
