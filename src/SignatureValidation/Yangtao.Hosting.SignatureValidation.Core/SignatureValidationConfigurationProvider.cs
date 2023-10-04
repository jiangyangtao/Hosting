using Microsoft.Extensions.Configuration;
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

        public HmacShaConfiguration GetHmacShaConfiguration()
        {
            if (_rsaPublicConfiguration == null) throw new NullReferenceException(nameof(HmacShaConfiguration));

            return _hmacShaConfiguration;
        }

        public RsaPrivateConfiguration GetRsaPrivateConfiguration()
        {
            if (_rsaPrivateConfiguration == null) throw new NullReferenceException(nameof(RsaPrivateConfiguration));

            return _rsaPrivateConfiguration;
        }

        public RsaPublicConfiguration GetRsaPublicConfiguration()
        {
            if (_rsaPublicConfiguration == null) throw new NullReferenceException(nameof(RsaPublicConfiguration));

            return _rsaPublicConfiguration;
        }
    }
}
