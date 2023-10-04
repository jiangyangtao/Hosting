using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    internal class ClientHmacShaProvider : HmacShaProviderBase
    {
        public ClientHmacShaProvider(IClientConfigurationProvider clientConfigurationProvider) 
            : base(clientConfigurationProvider.ClientValidationOptions.IsHmacShaSignature, clientConfigurationProvider.HmacShaOptions)
        { }
    }
}
