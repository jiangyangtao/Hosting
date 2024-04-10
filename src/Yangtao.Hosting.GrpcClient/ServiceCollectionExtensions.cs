using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yangtao.Hosting.GrpcClient.Interceptors;
using Yangtao.Hosting.GrpcClient.Options;
using Yangtao.Hosting.GrpcCore;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcClient
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddGrpcClientService<TClient>(this IServiceCollection services, Action<GrpcClientOptions> optionAction) where TClient : class
        {
            var clientOptions = new GrpcClientOptions();
            optionAction(clientOptions);

            var httpClientBuilder = services.AddGrpcClient<TClient>(options =>
             {
                 options.SetDefault(clientOptions);
             });

            if (clientOptions.UseAuthenticationGrpcClientInterceptor)
            {
                services.AddAuthenticationGrpcClientInterceptor();
                httpClientBuilder.AddAuthenticationGrpcClientInterceptor(clientOptions.InterceptorScope);
            }

            if (clientOptions.SignAuthenticationType.HasValue)
            {
                if (clientOptions.SignAuthenticationType == SignAuthenticationType.Aes && clientOptions.AesSignOptions == null)
                    throw new ArgumentException(nameof(clientOptions.AesSignOptions));

                if (clientOptions.SignAuthenticationType == SignAuthenticationType.RSA && clientOptions.RsaPublicSignOptions == null)
                    throw new ArgumentException(nameof(clientOptions.RsaPublicSignOptions));

                services.AddGrpcCore(options =>
                {
                    options.SignAuthenticationType = clientOptions.SignAuthenticationType;
                    options.AesSignOptions = clientOptions.AesSignOptions;
                    options.RsaPublicSignOptions = clientOptions.RsaPublicSignOptions;
                });

                services.AddSignAuthenticationGrpcClientInterceptor();
                httpClientBuilder.AddSignAuthenticationGrpcClientInterceptor(clientOptions.InterceptorScope);
            }

            return httpClientBuilder;
        }

        public static IServiceCollection AddAuthenticationGrpcClientInterceptor(this IServiceCollection services)
        {
            services.TryAddTransient<AuthenticationGrpcClientInterceptor>();

            return services;
        }

        public static IServiceCollection AddSignAuthenticationGrpcClientInterceptor(this IServiceCollection services)
        {
            services.TryAddTransient<SignAuthenticationGrpcClientInterceptor>();

            return services;
        }
    }
}
