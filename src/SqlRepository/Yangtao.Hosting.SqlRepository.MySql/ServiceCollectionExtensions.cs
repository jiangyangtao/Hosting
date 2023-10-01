using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SqlRepository.Core;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SqlRepository.Abstractions;

namespace Yangtao.Hosting.SqlRepository.MySql
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
            services.AddSingleton<ISqlRepository, MySqlRepository>();
            return services.AddRepositoryCore(optionAction);
        }

        private static string GetConnectionString(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionStringsConfig = configuration.GetSection("ConnectionStrings") ?? throw new KeyNotFoundException("In configuration not found ConnectionStrings");

            var mysqlConnectionStringConfig = connectionStringsConfig.GetSection("MySql");
            mysqlConnectionStringConfig ??= connectionStringsConfig.GetSection("Mysql");
            mysqlConnectionStringConfig ??= connectionStringsConfig.GetSection("mysql");

            if (mysqlConnectionStringConfig == null) throw new KeyNotFoundException("In configuration not found MySql or Mysql or mysql of ConnectionStrings");
            if (mysqlConnectionStringConfig.Value.IsNullOrEmpty()) throw new KeyNotFoundException("The ConnectionString can not be null or empty.");

            return mysqlConnectionStringConfig.Value;
        }
    }
}