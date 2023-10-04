using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Server
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddHcmaShaSignatureValidation(this IServiceCollection services, Action<HmacShaOptions> optionAction)
        {
            var hmacShaOptions = new HmacShaOptions();
            optionAction(hmacShaOptions);

            if (hmacShaOptions.SecretKey.IsNullOrEmpty()) throw new ArgumentNullException(null, nameof(HmacShaOptions.SecretKey));
            return services.AddServerValidation(ValidationType.Signatrue, SignatureAlgorithm.HmacSha, hmacShaOptions);
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

            if (rsaOptions.PrivateKey.IsNullOrEmpty()) throw new ArgumentNullException(null, nameof(RsaOptions.PrivateKey));

            return services.AddServerValidation(validationType, SignatureAlgorithm.RSA, rsaOptions: rsaOptions);
        }

        private static IServiceCollection AddServerValidation(this IServiceCollection services, ValidationType validationType, SignatureAlgorithm signatureAlgorithm,
            HmacShaOptions? hmacShaOptions = null, RsaOptions? rsaOptions = null)
        {
            services.Configure<ServerValidationOptions>(a =>
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
                    a.PrivateKey = rsaOptions.PrivateKey;
                });
            }

            services.AddSingleton<IHmacShaProvider, ServerHmacShaProvider>();
            services.AddSingleton<IRsaPrivateProvider, RsaPrivateProvider>();
            return services;
        }
    }
}