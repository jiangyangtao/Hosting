using Yangtao.Hosting.FrontendApi;

namespace Test.FrontApi.Application
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
            app.UseFrontendConfiguration(options =>
            {
                options.DefaultServiceName = "Test.FrontApi.Application";
                options.Endpoint = "api/v1/Frontend/Configuration";
                options.RequireAuthorization = false;
            });

            app.Run();
        }
    }
}
