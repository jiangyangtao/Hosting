using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClientSignatureValidation(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddClientEncryptionValidation(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddClientValidation(this IServiceCollection services)
        {
            services.AddSingleton<IRsaPublicProvider, RsaPublicProvider>();
            return services;
        }
    }
}