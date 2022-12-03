using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Cache.Redis.Abstracts;

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
