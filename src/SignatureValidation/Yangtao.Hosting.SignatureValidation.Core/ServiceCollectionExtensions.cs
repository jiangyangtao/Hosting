using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.HmacShaPorviders;
using Yangtao.Hosting.SignatureValidation.Core.RsaProviders;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSignatureValidationCore(this IServiceCollection services)
        {
            services.AddSingleton<IRsaPrivateProvider, RsaPrivateProvider>();
            services.AddSingleton<IRsaPublicProvider, RsaPublicProvider>();

            services.AddSingleton<IHmacShaPorviderFactory, HmacShaPorviderFactory>();
            services.AddSingleton<IHmacShaProvider, HmacSha256Provider>();
            services.AddSingleton<IHmacShaProvider, HmacSha384Provider>();
            services.AddSingleton<IHmacShaProvider, HmacSha512Provider>();

            services.AddSingleton<ISignatureValidationProvider, SignatureValidationProvider>();
            return services;
        }
    }
}
