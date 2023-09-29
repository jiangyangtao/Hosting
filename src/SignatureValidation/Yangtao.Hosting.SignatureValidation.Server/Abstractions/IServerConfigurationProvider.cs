using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server.Abstractions
{
    internal interface IServerConfigurationProvider
    {
        ServerValidationOptions ServerValidationOptions { get; }

        HmacShaOptions? HmacShaOptions { get; }

        RsaSignatureOptions? RsaSignatureOptions { get; }

        RsaEncryptionOptions? RsaEncryptionOptions { get; }
    }
}
