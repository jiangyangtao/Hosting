using Yangtao.Hosting.SignatureValidation.Server.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Server
{
    internal class ServerSignatureValidationProvider : IServerSignatureValidationProvider
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

        public string SignData(string value)
        {
            if (_serverConfigurationProvider.ServerValidationOptions.IsHmacShaSignature)
                return _serverHmacShaProvider.SignData(value);

            return _rsaPrivateProvider.SignData(value);
        }
    }
}
