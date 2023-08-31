using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Core;
using Yangtao.Hosting.Repository.Core.Builders;

namespace Yangtao.Hosting.Repository.SqlServer
{
    public static class SqlServerRepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            var connectionString = services.GetConnectionString();
            return services.AddRepository(connectionString);
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, Action<DbContextBuilder> builderAction)
        {
            var connectionString = services.GetConnectionString();
            return services.AddRepository(connectionString, builderAction);
        }

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

        private static string GetConnectionString(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionStringsConfig = configuration.GetSection("ConnectionStrings") ?? throw new KeyNotFoundException("In configuration not found ConnectionStrings");

            var mysqlConnectionStringConfig = connectionStringsConfig.GetSection("SqlServer");
            mysqlConnectionStringConfig ??= connectionStringsConfig.GetSection("sqlserver");
            mysqlConnectionStringConfig ??= connectionStringsConfig.GetSection("Sqlserver");

            if (mysqlConnectionStringConfig == null) throw new KeyNotFoundException("In configuration not found SqlServer or sqlserver or Sqlserver of ConnectionStrings");
            if (mysqlConnectionStringConfig.Value.IsNullOrEmpty()) throw new KeyNotFoundException("The ConnectionString can not be null or empty.");

            return mysqlConnectionStringConfig.Value;
        }
    }
}