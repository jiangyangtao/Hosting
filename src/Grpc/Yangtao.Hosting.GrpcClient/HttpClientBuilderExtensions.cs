using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.GrpcClient.Interceptors;

namespace Yangtao.Hosting.GrpcClient
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder AddAuthenticationGrpcClientInterceptor(this IHttpClientBuilder httpClientBuilder, InterceptorScope scope = InterceptorScope.Channel)
        {
            httpClientBuilder.AddInterceptor<AuthenticationGrpcClientInterceptor>(scope);

            return httpClientBuilder;
        }

        public static IHttpClientBuilder AddSignAuthenticationGrpcClientInterceptor(this IHttpClientBuilder httpClientBuilder, InterceptorScope scope = InterceptorScope.Channel)
        {
            httpClientBuilder.AddInterceptor<SignAuthenticationGrpcClientInterceptor>(scope);

            return httpClientBuilder;
        }
    }
}
