using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Runtime.InteropServices;

namespace Yangtao.Hosting.Swagger
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("", new OpenApiInfo
                {
                    //Version = version,
                    //Title = $"{ApiName} 接口文档——{RuntimeInformation.FrameworkDescription}",
                    //Description = $"{ApiName} HTTP API " + version,
                });
                options.OrderActionsBy(o => o.RelativePath);
            });
            return services;
        }
    }
}