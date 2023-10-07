using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;
using Yangtao.Hosting.SignatureValidation.Client.GrpcInterceptor;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder AddClientSignatureValidationGrpcInterceptor(this IHttpClientBuilder clientBuilder, Action<GrpcInterceptorOptions> optionAction)
        {
            var options = new GrpcInterceptorOptions();
            optionAction(options);

            clientBuilder.Services.Configure<GrpcInterceptorOptions>(a =>
            {
                a.InterceptorMethods = options.InterceptorMethods;
            });

            clientBuilder.Services.TryAddTransient<ClientSignatureValidationInterceptor>();
            clientBuilder.AddInterceptor<ClientSignatureValidationInterceptor>(InterceptorScope.Channel);
            return clientBuilder;
        }
    }
}
