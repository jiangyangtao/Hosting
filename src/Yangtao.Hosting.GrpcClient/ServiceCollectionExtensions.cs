using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yangtao.Hosting.GrpcClient.Interceptors;
using Yangtao.Hosting.GrpcClient.Options;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcClient
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddGrpcClientService<TClient>(this IServiceCollection services, Action<GrpcClientOptions> optionAction) where TClient : class
        {
            var clientOptions = new GrpcClientOptions();
            optionAction(clientOptions);

            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<SignAuthenticationOptions>(a =>
            {
                a.UseSignAuthenticationGrpcClientInterceptor = clientOptions.UseSignAuthenticationGrpcClientInterceptor;
                a.SignAuthenticationType = clientOptions.SignAuthenticationType;
            });

            var httpClientBuilder = services.AddGrpcClient<TClient>(options =>
             {
                 options.SetDefault(clientOptions);
             });

            if (clientOptions.UseAuthenticationGrpcClientInterceptor)
            {
                services.AddAuthenticationGrpcClientInterceptor();
                httpClientBuilder.AddAuthenticationGrpcClientInterceptor(clientOptions.InterceptorScope);
            }

            if (clientOptions.UseSignAuthenticationGrpcClientInterceptor)
            {
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
