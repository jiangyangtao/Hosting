using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SqlRepository.Abstractions;
using Yangtao.Hosting.SqlRepository.Core;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.SqlRepository.Sqlite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlRepository(this IServiceCollection services)
        {
            var connectionString = services.GetConnectionString();

            return services.AddSqlRepository(connectionString);
        }

        public static IServiceCollection AddSqlRepository(this IServiceCollection services, string connectionString)
        {
            return services.AddSqlRepository(options => options.ConnectionsString = connectionString);
        }

        public static IServiceCollection AddSqlRepository(this IServiceCollection services, Action<SqlRepositoryOptions> optionAction)
        {
            services.AddSingleton<ISqlRepository, SqliteRepository>();
            return services.AddRepositoryCore(optionAction);
        }

        private static string GetConnectionString(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionStringsConfig = configuration.GetSection("ConnectionStrings") ?? throw new KeyNotFoundException("In configuration not found ConnectionStrings");

            var mysqlConnectionStringConfig = connectionStringsConfig.GetSection("Sqlite");
            mysqlConnectionStringConfig ??= connectionStringsConfig.GetSection("sqlite");

            if (mysqlConnectionStringConfig == null) throw new KeyNotFoundException("In configuration not found Sqlite or sqlite of ConnectionStrings");
            if (mysqlConnectionStringConfig.Value.IsNullOrEmpty()) throw new KeyNotFoundException("The ConnectionString can not be null or empty.");

            return mysqlConnectionStringConfig.Value;
        }
    }
}