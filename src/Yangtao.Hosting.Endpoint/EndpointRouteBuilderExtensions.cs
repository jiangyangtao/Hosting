using Yangtao.Hosting.Endpoint.EnumConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Yangtao.Hosting.Endpoint
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder UseEnumConfigurationEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var options = new EnumConfigurationOptions();

            return UseEnumConfigurationEndpoint(endpointRouteBuilder, options);
        }

        public static IEndpointRouteBuilder UseEnumConfigurationEndpoint(this IEndpointRouteBuilder endpointRouteBuilder, Action<EnumConfigurationOptions> optionAction)
        {
            var options = new EnumConfigurationOptions();
            optionAction(options);

            return UseEnumConfigurationEndpoint(endpointRouteBuilder, options);
        }

        private static IEndpointRouteBuilder UseEnumConfigurationEndpoint(this IEndpointRouteBuilder endpointRouteBuilder, EnumConfigurationOptions options)
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