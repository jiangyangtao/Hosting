using Microsoft.Extensions.Options;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;
using Yangtao.Hosting.SignatureValidation.Core;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    internal class ClientHmacShaProvider : HmacShaProviderBase
    {
        public ClientHmacShaProvider(
            IOptions<ClientValidationOptions> clientValidationOptions,
            IOptions<HmacShaOptions> hmacShaOptions) : base(clientValidationOptions.Value.IsHmacShaSignature, hmacShaOptions.Value)
        { }
    }
}
