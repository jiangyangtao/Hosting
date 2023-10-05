using Yangtao.Hosting.SignatureValidation.Client.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Client.Abstractions
{
    internal interface IClientConfigurationProvider
    {
        ClientValidationOptions ClientValidationOptions { get; }

        HmacShaOptions? HmacShaOptions { get; }

        RsaSignatureOptions? RsaSignatureOptions { get; }

        RsaEncryptionOptions? RsaEncryptionOptions { get; }
    }
}
