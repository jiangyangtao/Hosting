using Yangtao.Hosting.SignatureValidation.Client.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    internal class ClientSignatureValidationProvider : IClientSignatureValidationProvider
    {
        private readonly IClientHmacShaProvider _clientHmacShaProvider;
        private readonly IRsaPublicProvider _rsaPublicProvider;
        private readonly IClientConfigurationProvider _clientConfigurationProvider;

        public ClientSignatureValidationProvider(
            IClientHmacShaProvider clientHmacShaProvider,
            IRsaPublicProvider rsaPublicProvider,
            IClientConfigurationProvider clientConfigurationProvider)
        {
            _clientHmacShaProvider = clientHmacShaProvider;
            _rsaPublicProvider = rsaPublicProvider;
            _clientConfigurationProvider = clientConfigurationProvider;
        }      

        public bool VerifyData(string value, string signature)
        {
            if (_clientConfigurationProvider.ClientValidationOptions.IsHmacShaSignature)
                return _clientHmacShaProvider.VerifyData(value, signature);

            return _rsaPublicProvider.VerifyData(value, signature);
        }
    }
}
