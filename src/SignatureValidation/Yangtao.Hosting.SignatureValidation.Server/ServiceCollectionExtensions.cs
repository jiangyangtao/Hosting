using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Server
{
    public static class ServiceCollectionExtensions
    {
        
        public static IServiceCollection AddServerHcmaShaSignatureValidation(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddServerRsaSignatureValidation(this IServiceCollection services)
        {
            return services;
        }


        public static IServiceCollection AddServerEncryptionValidation(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddServerSignature(this IServiceCollection services)
        {
            services.AddSingleton<IRsaPrivateProvider, RsaPrivateProvider>();
            return services;
        }
    }
}