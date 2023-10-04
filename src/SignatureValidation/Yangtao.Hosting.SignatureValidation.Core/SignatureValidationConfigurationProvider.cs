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

        public SignatureValidationConfigurationProvider(
            IOptions<SignatureValidationConfiguration> signatureValidationConfigurationOptions,
            IOptions<HmacShaConfiguration> hmacShaConfigurationOptions)
        {
            _signatureValidationConfiguration = signatureValidationConfigurationOptions.Value;
            _hmacShaConfiguration = hmacShaConfigurationOptions.Value;
        }

        public ValidationType ValidationType => _signatureValidationConfiguration.ValidationType;

        public SignatureAlgorithm SignatureAlgorithm => _signatureValidationConfiguration.SignatureAlgorithm;

        public bool IsHmacShaSignature => _signatureValidationConfiguration.IsHmacShaSignature;

        public HmacShaConfiguration HmacShaConfiguration => _hmacShaConfiguration;
    }
}
