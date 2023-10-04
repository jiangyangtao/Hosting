using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    internal class SignatureValidationProvider : ISignatureValidationProvider
    {
        private readonly ISignatureValidationConfigurationProvider _configurationProvider;
        private readonly IRsaPrivateProvider _rsaPrivateProvider;
        private readonly IRsaPublicProvider _rsaPublicProvider;
        private readonly IHmacShaProvider _hmacShaProvider;

        public SignatureValidationProvider(
            ISignatureValidationConfigurationProvider configurationProvider,
            IRsaPrivateProvider rsaPrivateProvider,
            IRsaPublicProvider rsaPublicProvider,
            IHmacShaProvider hmacShaProvider)
        {
            _configurationProvider = configurationProvider;
            _rsaPrivateProvider = rsaPrivateProvider;
            _rsaPublicProvider = rsaPublicProvider;
            _hmacShaProvider = hmacShaProvider;
        }

        public string Decrypt(string ciphertext) => _rsaPrivateProvider.Decrypt(ciphertext);

        public string Encrypt(string plaintext) => _rsaPublicProvider.Encrypt(plaintext);

        public string SignData(string value)
        {
            if (_configurationProvider.SignatureAlgorithm == SignatureAlgorithm.HmacSha) return _hmacShaProvider.SignData(value);

            return _rsaPrivateProvider.SignData(value);
        }

        public bool VerifyData(string value, string signature)
        {
            if (_configurationProvider.SignatureAlgorithm == SignatureAlgorithm.HmacSha) return _hmacShaProvider.VerifyData(value, signature);

            return _rsaPublicProvider.VerifyData(value, signature);
        }
    }
}
