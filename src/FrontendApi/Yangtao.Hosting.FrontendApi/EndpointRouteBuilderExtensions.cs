using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace Yangtao.Hosting.FrontendApi
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder UseFrontendConfiguration(this IEndpointRouteBuilder routeBuilder)
        {
            var options = new FrontendApiConfigurationOptions();
            return routeBuilder.UseFrontendConfiguration(options);

        }

        public static IEndpointRouteBuilder UseFrontendConfiguration(this IEndpointRouteBuilder routeBuilder, Action<FrontendApiConfigurationOptions> optionAction)
        {
            var options = new FrontendApiConfigurationOptions();
            optionAction(options);
            return routeBuilder.UseFrontendConfiguration(options);
        }

        private static IEndpointRouteBuilder UseFrontendConfiguration(this IEndpointRouteBuilder endpointRouteBuilder, FrontendApiConfigurationOptions options)
        {
            var result = new FrontendComponentBuilder(options.DefaultServiceName).BuildJson();
            var routeHandlerBuilder = endpointRouteBuilder.MapGet(options.Endpoint, async (HttpResponse response) =>
            {
                response.ContentType = options.ResponseContentType;
                await response.WriteAsync(result);
            });

            if (options.RequireAuthorization) routeHandlerBuilder.RequireAuthorization();
            return endpointRouteBuilder;
        }
    }
}
