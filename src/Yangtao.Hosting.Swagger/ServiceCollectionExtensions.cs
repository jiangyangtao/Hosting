using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.Swagger
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                
            });
            return services;
        }
    }
}