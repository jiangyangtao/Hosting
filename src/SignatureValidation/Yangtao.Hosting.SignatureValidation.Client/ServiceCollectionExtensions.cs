using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;
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

        public static IServiceCollection AddRsaSignatureValidation(this IServiceCollection services, Action<RsaSignatureOptions> optionAction)
        {
            var rsaSignatureOptions = new RsaSignatureOptions();
            optionAction(rsaSignatureOptions);
            if (rsaSignatureOptions.PublicKey.IsNullOrEmpty()) throw new ArgumentNullException(null, nameof(RsaSignatureOptions.PublicKey));

            return services.AddClientValidation(ValidationType.Encrypt, SignatureAlgorithm.RSA, rsaSignatureOptions: rsaSignatureOptions);
        }

        public static IServiceCollection AddEncryptionValidation(this IServiceCollection services, Action<RsaEncryptionOptions> optionAction)
        {
            var rsaEncryptionOptions = new RsaEncryptionOptions();
            optionAction(rsaEncryptionOptions);
            if (rsaEncryptionOptions.PublicKey.IsNullOrEmpty()) throw new ArgumentNullException(null, nameof(RsaEncryptionOptions.PublicKey));

            return services.AddClientValidation(ValidationType.Encrypt, SignatureAlgorithm.RSA, rsaEncryptionOptions: rsaEncryptionOptions);
        }

        private static IServiceCollection AddClientValidation(this IServiceCollection services, ValidationType validationType, SignatureAlgorithm signatureAlgorithm,
            HmacShaOptions? hmacShaOptions = null, RsaSignatureOptions? rsaSignatureOptions = null, RsaEncryptionOptions? rsaEncryptionOptions = null)
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

            if (rsaSignatureOptions != null)
            {
                services.Configure<RsaSignatureOptions>(a =>
                {
                    a.RSAKeyType = rsaSignatureOptions.RSAKeyType;
                    a.RSASignaturePaddingMode = rsaSignatureOptions.RSASignaturePaddingMode;
                    a.PublicKey = rsaSignatureOptions.PublicKey;
                });
            }

            if (rsaEncryptionOptions != null)
            {
                services.Configure<RsaEncryptionOptions>(a =>
                {
                    a.RSAKeyType = rsaEncryptionOptions.RSAKeyType;
                    a.RSAEncryptionPaddingType = rsaEncryptionOptions.RSAEncryptionPaddingType;
                    a.PublicKey = rsaEncryptionOptions.PublicKey;
                });
            }

            services.AddSingleton<IClientConfigurationProvider, ClientConfigurationProvider>();
            services.AddSingleton<IClientSignatureValidationProvider, ClientSignatureValidationProvider>();
            services.AddSingleton<IClientHmacShaProvider, ClientHmacShaProvider>();
            services.AddSingleton<IRsaPublicProvider, RsaPublicProvider>();
            return services;
        }
    }
}