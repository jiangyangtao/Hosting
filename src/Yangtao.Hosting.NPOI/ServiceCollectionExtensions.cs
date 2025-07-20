using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.NPOI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExcelService(this IServiceCollection services)
        {
            services.AddSingleton<IExcelService, ExcelService>();

            return services;
        }
    }
}
