using Microsoft.Extensions.Options;
using Yangtao.Hosting.SignatureValidation.Core;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server
{
    internal class ServerHmacShaProvider: HmacShaProviderBase
    {
        public ServerHmacShaProvider(
          IOptions<ServerValidationOptions> serverValidationOptions,
          IOptions<HmacShaOptions> hmacShaOptions) : base(serverValidationOptions.Value.IsHmacShaSignature, hmacShaOptions.Value)
        { }
    }
}
