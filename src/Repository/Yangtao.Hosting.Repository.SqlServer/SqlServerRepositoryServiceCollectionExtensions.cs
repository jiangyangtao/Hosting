using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                options.UseLazyLoadingProxies().UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
            });

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString, Action<DbContextBuilder> builderAction)
        {
            services.AddRepositoryCore(options => options.UseLazyLoadingProxies().
                                                        UseSqlServer(connectionString).
                                                        LogTo(Console.WriteLine, LogLevel.Information).
                                                        EnableSensitiveDataLogging(), builderAction);
            return services;
        }

        private static string GetConnectionString(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetConnectionString("SqlServer") ??
                                       configuration.GetConnectionString("sqlserver") ??
                                       configuration.GetConnectionString("Sqlserver") ??
                                       throw new KeyNotFoundException("In configuration not found SqlServer or sqlserver or Sqlserver of ConnectionStrings");
        }
    }
}