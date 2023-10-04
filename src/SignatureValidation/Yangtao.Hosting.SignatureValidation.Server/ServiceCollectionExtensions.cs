using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddHcmaShaSignatureValidation(this IServiceCollection services, Action<HmacShaOptions> optionAction)
        {
            return services;
        }

        public static IServiceCollection AddRsaSignatureValidation(this IServiceCollection services, Action<RsaOptions> optionAction)
        {
            return services;
        }


        public static IServiceCollection AddEncryptionValidation(this IServiceCollection services, Action<RsaOptions> optionAction)
        {
            return services;
        }

        private static IServiceCollection AddServerSignature(this IServiceCollection services)
        {
            services.AddSingleton<IRsaPrivateProvider, RsaPrivateProvider>();
            return services;
        }
    }
}