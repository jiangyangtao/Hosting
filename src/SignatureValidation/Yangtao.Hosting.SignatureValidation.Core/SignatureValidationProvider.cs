using Yangtao.Hosting.SignatureValidation.Core.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    internal class SignatureValidationProvider : ISignatureValidationProvider
    {
        private readonly ISignatureValidationConfigurationProvider _configurationProvider;

        public SignatureValidationProvider(ISignatureValidationConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public string Decrypt(string ciphertext)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plaintext)
        {
            throw new NotImplementedException();
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
