using Yangtao.Hosting.SignatureValidation.Core.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    internal class RsaPublicProvider : IRsaPublicProvider
    {
        public RsaPublicProvider()
        {
        }

        public string Encrypt(string plaintext)
        {
            throw new NotImplementedException();
        }

        public bool VerifyData(string value, string signature)
        {
            throw new NotImplementedException();
        }
    }
}
