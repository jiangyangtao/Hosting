using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Repository.Core;

namespace Yangtao.Hosting.Repository.MySql
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString)
        {
            services.AddRepositoryCore(options => options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString, Action<DbContextBuilder> builderAction)
        {
            services.AddRepositoryCore(options => options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), builderAction);
            return services;
        }
    }
}