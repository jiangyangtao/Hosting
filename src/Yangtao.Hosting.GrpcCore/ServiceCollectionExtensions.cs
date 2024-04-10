using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.GrpcCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGrpcCore(this IServiceCollection services)
        {
            services.AddSingleton<ISignAuthenticationProvider, SignAuthenticationProvider>();
            return services;
        }
    }
}
