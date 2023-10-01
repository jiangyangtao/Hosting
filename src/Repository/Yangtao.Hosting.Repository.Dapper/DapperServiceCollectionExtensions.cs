using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.Repository.Dapper
{
    public static class DapperServiceCollectionExtensions
    {
        public static IServiceCollection AddDapperRepository(this IServiceCollection services, Action<DapperOptions> optionAction)
        {
            var options = new DapperOptions();
            optionAction(options);
            services.Configure<DapperOptions>(a =>
            {
                a.DbConnectionString = options.DbConnectionString;
            });

            services.AddSingleton<ISqlRepository, SqlRepository>();
            return services;
        }
    }
}