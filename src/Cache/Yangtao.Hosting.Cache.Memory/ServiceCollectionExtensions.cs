using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Cache.Memory.Abstracts;

namespace Yangtao.Hosting.Cache.Memory
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMemoryCache(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var memoryCacheConfig = configuration.GetSection("MemoryCache") ?? throw new KeyNotFoundException("In configuration not found MemoryCache");
            var sizeConfig = memoryCacheConfig.GetSection("SizeLimit");
            sizeConfig ??= memoryCacheConfig.GetSection("sizeLimit");
            sizeConfig ??= memoryCacheConfig.GetSection("size");
            sizeConfig ??= memoryCacheConfig.GetSection("Limit");
            sizeConfig ??= memoryCacheConfig.GetSection("limit");

            if (sizeConfig == null) throw new KeyNotFoundException("In configuration not found SizeLimit or sizeLimit or size or Limit or limit of MemoryCache");

            var sizeString = sizeConfig.Get<string>();
            var parseResult = int.TryParse(sizeString, out int size);
            if (parseResult == false) throw new KeyNotFoundException("The MemoryCache limit of capacity must be integer.");

            return services.AddMemoryCache(size);
        }

        public static IServiceCollection AddMemoryCache(this IServiceCollection services, int sizeLimit)
        {
            services.AddMemoryCache(options =>
            {
                if (sizeLimit > 0) options.SizeLimit = sizeLimit;
            });
            services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();
            return services;
        }
    }
}
