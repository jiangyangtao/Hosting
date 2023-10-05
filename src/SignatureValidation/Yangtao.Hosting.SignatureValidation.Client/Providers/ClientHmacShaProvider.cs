using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core;

namespace Yangtao.Hosting.SignatureValidation.Client.Providers
{
    internal class ClientHmacShaProvider : HmacShaProviderBase, IClientHmacShaProvider
    {
        public ClientHmacShaProvider(IClientConfigurationProvider clientConfigurationProvider)
            : base(clientConfigurationProvider.ClientValidationOptions.IsHmacShaSignature, clientConfigurationProvider.HmacShaOptions)
        { }
    }
}
