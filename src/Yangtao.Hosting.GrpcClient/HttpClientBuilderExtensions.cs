using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.GrpcClient
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder AddAuthenticationGrpcClientInterceptor(this IHttpClientBuilder httpClientBuilder, InterceptorScope scope = InterceptorScope.Channel)
        {
            httpClientBuilder.AddInterceptor<AuthenticationGrpcClientInterceptor>(scope);

            return httpClientBuilder;
        }
    }
}
