using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Yangtao.Hosting.GrpcClient
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationGrpcClientInterceptor(this IServiceCollection services)
        {
            services.TryAddTransient<AuthenticationGrpcClientInterceptor>();

            return services;
        }

        public static IServiceCollection AddGrpcClientService<TClient>(this IServiceCollection services, Action<GrpcClientOptions> optionAction) where TClient : class
        {
            var clientOptions = new GrpcClientOptions();
            optionAction(clientOptions);

            var httpClientBuilder = services.AddGrpcClient<TClient>(options =>
             {
                 options.SetDefault(clientOptions);
             }).AddAuthenticationGrpcClientInterceptor();

            if (clientOptions.UseAuthenticationGrpcClientInterceptor)
            {
                services.AddAuthenticationGrpcClientInterceptor();
                httpClientBuilder.AddAuthenticationGrpcClientInterceptor();
            }

            return services;
        }
    }
}
