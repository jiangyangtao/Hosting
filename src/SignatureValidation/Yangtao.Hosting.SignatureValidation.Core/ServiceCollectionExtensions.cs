using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.RsaProviders;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSignatureValidationCore(this IServiceCollection services)
        {
            services.AddSingleton<IRsaPrivateProvider, RsaPrivateProvider>();
            services.AddSingleton<IRsaPublicProvider, RsaPublicProvider>();
            services.AddSingleton<IHmacShaProvider, HmacShaProvider>();
            services.AddSingleton<ISignatureValidationFactory, SignatureValidationFactory>();
            services.AddSingleton<ISignatureValidationProvider, SignatureValidationProvider>();
            return services;
        }
    }
}
