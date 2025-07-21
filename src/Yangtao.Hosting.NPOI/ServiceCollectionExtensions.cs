using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.NPOI.Abstractions;

namespace Yangtao.Hosting.NPOI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExcel(this IServiceCollection services)
        {
            services.AddSingleton<IExcelService, ExcelService>();

            return services;
        }
    }
}
