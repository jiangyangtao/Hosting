using Microsoft.Extensions.DependencyInjection;
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

            clientBuilder.AddInterceptor<ClientSignatureValidationInterceptor>();
            return clientBuilder;
        }
    }
}
