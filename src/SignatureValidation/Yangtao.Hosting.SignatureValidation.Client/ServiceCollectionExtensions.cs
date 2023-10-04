using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHcmaShaSignatureValidation(this IServiceCollection services, Action<HmacShaOptions> optionAction)
        {
            var hmacShaOptions = new HmacShaOptions();
            optionAction(hmacShaOptions);

            if (hmacShaOptions.SecretKey.IsNullOrEmpty()) throw new ArgumentNullException(null, nameof(HmacShaOptions.SecretKey));
            return services.AddClientValidation(ValidationType.Signatrue, SignatureAlgorithm.HmacSha, hmacShaOptions);
        }

        public static IServiceCollection AddRsaSignatureValidation(this IServiceCollection services, Action<RsaOptions> optionAction)
        {
            return services.AddRsaValidation(ValidationType.Signatrue, optionAction);
        }

        public static IServiceCollection AddEncryptionValidation(this IServiceCollection services, Action<RsaOptions> optionAction)
        {
            return services.AddRsaValidation(ValidationType.Encrypt, optionAction);
        }

        private static IServiceCollection AddRsaValidation(this IServiceCollection services, ValidationType validationType, Action<RsaOptions> optionAction)
        {
            var rsaOptions = new RsaOptions();
            optionAction(rsaOptions);

            if (rsaOptions.PublicKey.IsNullOrEmpty()) throw new ArgumentNullException(null, nameof(RsaOptions.PublicKey));

            return services.AddClientValidation(validationType, SignatureAlgorithm.RSA, rsaOptions: rsaOptions);
        }

        private static IServiceCollection AddClientValidation(this IServiceCollection services, ValidationType validationType, SignatureAlgorithm signatureAlgorithm,
            HmacShaOptions? hmacShaOptions = null, RsaOptions? rsaOptions = null)
        {
            services.Configure<ClientValidationOptions>(a =>
            {
                a.ValidationType = validationType;
                a.SignatureAlgorithm = signatureAlgorithm;
            });

            if (hmacShaOptions != null)
            {
                services.Configure<HmacShaOptions>(a =>
                {
                    a.HmacShaAlgorithmType = hmacShaOptions.HmacShaAlgorithmType;
                    a.HmacShaSignatureFormatType = hmacShaOptions.HmacShaSignatureFormatType;
                    a.SecretKey = hmacShaOptions.SecretKey;
                });
            }

            if (rsaOptions != null)
            {
                services.Configure<RsaOptions>(a =>
                {
                    a.RSAKeyType = rsaOptions.RSAKeyType;
                    a.RSAEncryptionPaddingType = rsaOptions.RSAEncryptionPaddingType;
                    a.PublicKey = rsaOptions.PublicKey;
                });
            }

            services.AddSingleton<IHmacShaProvider, ClientHmacShaProvider>();
            services.AddSingleton<IRsaPublicProvider, RsaPublicProvider>();
            return services;
        }
    }
}