using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.GrpcCore.Options;

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
