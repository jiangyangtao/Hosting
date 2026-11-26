using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            var loggerFactory = endpointRouteBuilder.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("FrontendApi");

            var componentBuilder = new FrontendComponentBuilder(options, logger);
            var routeHandlerBuilder = endpointRouteBuilder.MapGet(options.Endpoint, async (HttpResponse response, CancellationToken cancellationToken) =>
            {
                var jsonResult = componentBuilder.GetJsonResult();
                response.ContentType = options.ResponseContentType;
                await response.WriteAsync(jsonResult, cancellationToken);
                await response.CompleteAsync();
            });

            if (options.RequireAuthorization) routeHandlerBuilder.RequireAuthorization();

            logger.LogInformation("Registered FrontendApi, ServiceName:{ServiceName}.", options.DefaultServiceName);
            return endpointRouteBuilder;
        }
    }
}
