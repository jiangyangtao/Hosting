using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.SqlRepository.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryCore(this IServiceCollection services, string connectionString)
        {
            return services.AddRepositoryCore(options => options.ConnectionsString = connectionString);
        }

        public static IServiceCollection AddRepositoryCore(this IServiceCollection services, Action<SqlRepositoryOptions> optionAction)
        {
            var options = new SqlRepositoryOptions();
            services.Configure<SqlRepositoryOptions>(a =>
            {
                a.ConnectionsString = options.ConnectionsString;
            });
            return services;
        }
    }
}
