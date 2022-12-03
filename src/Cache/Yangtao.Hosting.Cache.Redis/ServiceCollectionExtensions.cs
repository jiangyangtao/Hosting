using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.Cache.Redis
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IRedisCacheContainer>(serviceProvider =>
            {
                return new RedisCacheContainer(serviceProvider, connectionString);
            });
            return services;
        }
    }
}
