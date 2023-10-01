using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.SqlRepository.Dapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDapperRepository(this IServiceCollection services, Action<SqlRepositoryOptions> optionAction)
        {
            var options = new SqlRepositoryOptions();
            optionAction(options);
            services.Configure<SqlRepositoryOptions>(a =>
            {
                a.DbConnectionString = options.DbConnectionString;
            });

            services.AddSingleton<ISqlRepository, SqlRepository>();
            return services;
        }
    }
}