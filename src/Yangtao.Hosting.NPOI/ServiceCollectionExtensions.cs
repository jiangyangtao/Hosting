using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.NPOI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExcelServices(this IServiceCollection services)
        {
            services.AddSingleton<IExcelService, ExcelService>();

            return services;
        }
    }
}
