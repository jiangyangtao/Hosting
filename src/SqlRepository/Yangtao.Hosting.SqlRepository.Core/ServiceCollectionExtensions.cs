using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SqlRepository.Abstractions;

namespace Yangtao.Hosting.SqlRepository.Core
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