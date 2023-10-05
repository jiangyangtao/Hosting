using Microsoft.Extensions.Options;
using Yangtao.Hosting.SignatureValidation.Core;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server.Providers
{
    internal class ServerHmacShaProvider : HmacShaProviderBase, IServerHmacShaProvider
    {
        public ServerHmacShaProvider(
          IOptions<ServerValidationOptions> serverValidationOptions,
          IOptions<HmacShaOptions> hmacShaOptions) : base(serverValidationOptions.Value.IsHmacShaSignature, hmacShaOptions.Value)
        { }
    }
}
