using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Mvc.Abstractions;

namespace Yangtao.Hosting.Mvc
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationDocument(this IApplicationBuilder applicationBuilder)
        {
            var documentProvider = applicationBuilder.ApplicationServices.GetRequiredService<IApplicationDocumentProvider>();
            documentProvider.InitDocument();
            return applicationBuilder;
        }
    }
}
