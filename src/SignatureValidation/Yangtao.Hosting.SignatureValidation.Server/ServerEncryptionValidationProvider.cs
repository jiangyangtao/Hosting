using Yangtao.Hosting.SignatureValidation.Server.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Server
{
    internal class ServerEncryptionValidationProvider : IServerEncryptionValidationProvider
    {
        private readonly IRsaPrivateProvider _rsaPrivateProvider;

        public ServerEncryptionValidationProvider(IRsaPrivateProvider rsaPrivateProvider)
        {
            _rsaPrivateProvider = rsaPrivateProvider;
        }

        public string Decrypt(string ciphertext) => _rsaPrivateProvider.Decrypt(ciphertext);
    }
}
