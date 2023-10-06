using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;
using Yangtao.Hosting.SignatureValidation.Server.Middlewares;
using Yangtao.Hosting.SignatureValidation.Server.Providers;

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

        public static IServiceCollection AddRsaSignatureValidation(this IServiceCollection services, Action<RsaSignatureOptions> optionAction)
        {
            var rsaSignatureOptions = new RsaSignatureOptions();
            optionAction(rsaSignatureOptions);
            if (rsaSignatureOptions.PrivateKey.IsNullOrEmpty()) throw new ArgumentNullException(null, nameof(RsaSignatureOptions.PrivateKey));

            return services.AddServerValidation(ValidationType.Signatrue, SignatureAlgorithm.RSA, rsaSignatureOptions: rsaSignatureOptions);
        }

        public static IServiceCollection AddEncryptionValidation(this IServiceCollection services, Action<RsaEncryptionOptions> optionAction)
        {
            var rsaEncryptionOptions = new RsaEncryptionOptions();
            optionAction(rsaEncryptionOptions);
            if (rsaEncryptionOptions.PrivateKey.IsNullOrEmpty()) throw new ArgumentNullException(null, nameof(RsaEncryptionOptions.PrivateKey));

            return services.AddServerValidation(ValidationType.Encrypt, SignatureAlgorithm.RSA, rsaEncryptionOptions: rsaEncryptionOptions);
        }

        private static IServiceCollection AddServerValidation(this IServiceCollection services, ValidationType validationType, SignatureAlgorithm signatureAlgorithm,
            HmacShaOptions? hmacShaOptions = null, RsaSignatureOptions? rsaSignatureOptions = null, RsaEncryptionOptions? rsaEncryptionOptions = null)
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

            if (rsaSignatureOptions != null)
            {
                services.Configure<RsaSignatureOptions>(a =>
                {
                    a.RSAKeyType = rsaSignatureOptions.RSAKeyType;
                    a.RSASignaturePaddingMode = rsaSignatureOptions.RSASignaturePaddingMode;
                    a.PrivateKey = rsaSignatureOptions.PrivateKey;
                });
            }

            if (rsaEncryptionOptions != null)
            {
                services.Configure<RsaEncryptionOptions>(a =>
                {
                    a.RSAKeyType = rsaEncryptionOptions.RSAKeyType;
                    a.RSAEncryptionPaddingType = rsaEncryptionOptions.RSAEncryptionPaddingType;
                    a.PrivateKey = rsaEncryptionOptions.PrivateKey;
                });
            }

            services.TryAddSingleton<IServerConfigurationProvider, ServerConfigurationProvider>();
            services.TryAddSingleton<IServerEncryptionValidationProvider, ServerSignatureValidationProvider>();
            services.TryAddSingleton<IServerSignatureValidationProvider, ServerSignatureValidationProvider>();
            services.TryAddSingleton<IServerHmacShaProvider, ServerHmacShaProvider>();
            services.TryAddSingleton<IRsaPrivateProvider, RsaPrivateProvider>();
            services.TryAddSingleton<ServerSignatureValidationMiddleware>();
            return services;
        }
    }
}