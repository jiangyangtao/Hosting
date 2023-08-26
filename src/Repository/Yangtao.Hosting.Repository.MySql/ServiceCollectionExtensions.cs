using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Repository.Core;

namespace Yangtao.Hosting.Repository.MySql
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
        {
            services.AddRepository(options =>
            {
                var serverVersion = ServerVersion.AutoDetect(connectionString);
                options.UseLazyLoadingProxies().UseMySql(connectionString, serverVersion);
            });

            return services;
        }
    }
}