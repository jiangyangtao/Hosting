using Yangtao.Hosting.SignatureValidation.Client.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    internal class ClientEncryptionValidationProvider: IClientEncryptionValidationProvider
    {
        private readonly IRsaPublicProvider _rsaPublicProvider;

        public ClientEncryptionValidationProvider(IRsaPublicProvider rsaPublicProvider)
        {
            _rsaPublicProvider = rsaPublicProvider;
        }

        public string Encrypt(string plaintext) => _rsaPublicProvider.Encrypt(plaintext);
    }
}
