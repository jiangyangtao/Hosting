using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Cache.Redis.Abstracts;

namespace Yangtao.Hosting.Cache.Redis
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var connectionStringsConfig = configuration.GetSection("ConnectionStrings") ?? throw new KeyNotFoundException("In configuration not found ConnectionStrings");

            var redisConfig = connectionStringsConfig.GetSection("Redis");
            redisConfig ??= connectionStringsConfig.GetSection("redis");

            if (redisConfig == null) throw new KeyNotFoundException("In configuration not found Redis or redis of ConnectionStrings");
            if (redisConfig.Value.IsNullOrEmpty()) throw new KeyNotFoundException("The ConnectionString can not be null or empty.");

            return services.AddRedisCache(redisConfig.Value);
        }

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
