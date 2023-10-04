using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSignatureValidationCore(this IServiceCollection services)
        {
            services.AddSingleton<IHmacShaProvider, HmacShaProvider>();
            services.AddSingleton<ISignatureValidationProvider, SignatureValidationProvider>();
            return services;
        }
    }
}
