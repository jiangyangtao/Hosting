using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Repository.Core;
using Yangtao.Hosting.Repository.Core.Builders;

namespace Yangtao.Hosting.Repository.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
        {
            services.AddRepositoryCore(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(connectionString);
            });

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString, Action<DbContextBuilder> builderAction)
        {
            services.AddRepositoryCore(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString), builderAction);
            return services;
        }
    }
}