using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yangtao.Hosting.Repository.Core;
using Yangtao.Hosting.Repository.Core.Builders;

namespace Yangtao.Hosting.Repository.MySql
{
    public static class MySqlRepositoryServiceCollectionExtensions
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
            services.AddRepositoryCore(options => options.UseLazyLoadingProxies()
                                                        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                                                        .LogTo(Console.WriteLine, LogLevel.Information)
                                                        .EnableSensitiveDataLogging());
            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, string connectionString, Action<DbContextBuilder> builderAction)
        {
            services.AddRepositoryCore(options => options.UseLazyLoadingProxies()
                                                        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                                                        .LogTo(Console.WriteLine, LogLevel.Information)
                                                        .EnableSensitiveDataLogging(), builderAction);
            return services;
        }

        private static string GetConnectionString(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetConnectionString("MySql") ??
                                            configuration.GetConnectionString("mysql") ??
                                            configuration.GetConnectionString("Mysql") ??
                                            throw new KeyNotFoundException("In configuration not found MySql or Mysql or mysql of ConnectionStrings");
        }
    }
}