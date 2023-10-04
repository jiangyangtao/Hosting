using Microsoft.Extensions.Options;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    internal class SignatureValidationConfigurationProvider : ISignatureValidationConfigurationProvider
    {
        private readonly SignatureValidationConfiguration _signatureValidationConfiguration;
        private readonly HmacShaConfiguration _hmacShaConfiguration;
        private readonly RsaPrivateConfiguration _rsaPrivateConfiguration;
        private readonly RsaPublicConfiguration _rsaPublicConfiguration;

        public SignatureValidationConfigurationProvider(
            IOptions<SignatureValidationConfiguration> signatureValidationConfigurationOptions,
            IOptions<HmacShaConfiguration> hmacShaConfigurationOptions,
            IOptions<RsaPrivateConfiguration> rsaPrivateConfigurationOptions,
            IOptions<RsaPublicConfiguration> rsaPublicConfigurationOptions)
        {
            _signatureValidationConfiguration = signatureValidationConfigurationOptions.Value;
            _hmacShaConfiguration = hmacShaConfigurationOptions.Value;
            _rsaPrivateConfiguration = rsaPrivateConfigurationOptions.Value;
            _rsaPublicConfiguration = rsaPublicConfigurationOptions.Value;
        }

        public ValidationType ValidationType => _signatureValidationConfiguration.ValidationType;

        public SignatureAlgorithm SignatureAlgorithm => _signatureValidationConfiguration.SignatureAlgorithm;

        public bool IsHmacShaSignature => _signatureValidationConfiguration.IsHmacShaSignature;

        public bool IsRsaSignature => _signatureValidationConfiguration.IsRsaSignature;

        public bool IsRsaEncryption => _signatureValidationConfiguration.IsRsaEncryption;

        public HmacShaConfiguration GetHmacShaConfiguration()
        {
            return _hmacShaConfiguration ?? throw new NullReferenceException(nameof(HmacShaConfiguration));
        }

        public RsaPrivateConfiguration GetRsaPrivateConfiguration()
        {
            return _rsaPrivateConfiguration ?? throw new NullReferenceException(nameof(RsaPrivateConfiguration));
        }

        public RsaPublicConfiguration GetRsaPublicConfiguration()
        {
            return _rsaPublicConfiguration ?? throw new NullReferenceException(nameof(RsaPublicConfiguration));
        }
    }
}
