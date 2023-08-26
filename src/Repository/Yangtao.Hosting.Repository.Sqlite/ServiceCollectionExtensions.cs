using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Repository.Core;

namespace Yangtao.Hosting.Repository.Sqlite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
        {
            services.AddRepository(options =>
            {
                options.UseLazyLoadingProxies().UseSqlite(connectionString);
            });

            return services;
        }
    }
}