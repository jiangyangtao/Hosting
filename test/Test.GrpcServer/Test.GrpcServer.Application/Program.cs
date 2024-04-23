using Microsoft.Extensions.Configuration;
using Test.GrpcServer.Application.GrpcServices;
using Yangtao.Hosting.GrpcCore.Options;
using Yangtao.Hosting.GrpcServer;

namespace Test.GrpcServer.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var configuration = builder.Configuration;

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddGrpcServer(options =>
            {
                options.GrpcServiceOptions.EnableDetailedErrors = true;
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

            app.MapGrpcService<AuthenticationSerivce>();
            app.MapControllers();

            app.Run();
        }
    }
}
