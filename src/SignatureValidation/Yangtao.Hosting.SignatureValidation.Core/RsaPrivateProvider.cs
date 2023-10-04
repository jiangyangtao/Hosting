using Yangtao.Hosting.SignatureValidation.Core.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    internal class RsaPrivateProvider : IRsaPrivateProvider
    {
        public RsaPrivateProvider(ISignatureValidationConfigurationProvider configurationProvider)
        {

        }

        public string Decrypt(string ciphertext)
        {
            throw new NotImplementedException();
        }

        public string SignData(string value)
        {
            throw new NotImplementedException();
        }
    }
}
