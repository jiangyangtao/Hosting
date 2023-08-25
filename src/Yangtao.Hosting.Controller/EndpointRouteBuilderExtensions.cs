using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Yangtao.Hosting.Controller.EnumConfiguration;

namespace Yangtao.Hosting.Controller
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder UseEnumConfiguration(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var options = new EnumConfigurationOptions();

            return UseEnumConfiguration(endpointRouteBuilder, options);
        }

        public static IEndpointRouteBuilder UseEnumConfiguration(this IEndpointRouteBuilder endpointRouteBuilder, Action<EnumConfigurationOptions> optionAction)
        {
            var options = new EnumConfigurationOptions();
            optionAction(options);

            return UseEnumConfiguration(endpointRouteBuilder, options);
        }

        private static IEndpointRouteBuilder UseEnumConfiguration(this IEndpointRouteBuilder endpointRouteBuilder, EnumConfigurationOptions options)
        {
            var enumConfiguration = new EnumConfigurationBuilder(options).GetEnumConfiguration();
            var routeHandlerBuilder = endpointRouteBuilder.MapGet(options.EnumConfigurationEndpoint, async (HttpResponse response) =>
            {
                response.ContentType = options.ResponseContentType;
                await response.WriteAsync(enumConfiguration);
            });

            if (options.RequireAuthorization) routeHandlerBuilder.RequireAuthorization();
            return endpointRouteBuilder;
        }
    }
}
