using Yangtao.Hosting.SignatureValidation.Server.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Server.Providers
{
    internal class ServerSignatureValidationProvider : IServerSignatureValidationProvider, IServerEncryptionValidationProvider
    {
        private readonly IServerHmacShaProvider _serverHmacShaProvider;
        private readonly IRsaPrivateProvider _rsaPrivateProvider;
        private readonly IServerConfigurationProvider _serverConfigurationProvider;

        public ServerSignatureValidationProvider(
             IServerHmacShaProvider serverHmacShaProvider,
            IRsaPrivateProvider rsaPrivateProvider,
            IServerConfigurationProvider serverConfigurationProvider)
        {
            _serverHmacShaProvider = serverHmacShaProvider;
            _rsaPrivateProvider = rsaPrivateProvider;
            _serverConfigurationProvider = serverConfigurationProvider;
        }

        public string Decrypt(string ciphertext) => _rsaPrivateProvider.Decrypt(ciphertext);

        public string SignData(string value)
        {
            if (_serverConfigurationProvider.ServerValidationOptions.IsHmacShaSignature)
                return _serverHmacShaProvider.SignData(value);

            return _rsaPrivateProvider.SignData(value);
        }
    }
}
