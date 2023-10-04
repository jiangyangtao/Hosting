using Yangtao.Hosting.SignatureValidation.Core.Abstractions;

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

        public string Decrypt(string ciphertext)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plaintext)
        {
           

            return _rsaPublicProvider
        }
         
        public string SignData(string value)
        {
            throw new NotImplementedException();
        }

        public bool VerifyData(string value, string signature)
        {
            throw new NotImplementedException();
        }
    }
}
