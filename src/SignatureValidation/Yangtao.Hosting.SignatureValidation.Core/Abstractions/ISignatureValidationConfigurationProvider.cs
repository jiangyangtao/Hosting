using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Abstractions
{
    public interface ISignatureValidationConfigurationProvider
    {
        ValidationType ValidationType { get; }

        SignatureAlgorithm SignatureAlgorithm { get; }

        HmacShaConfiguration? HmacShaConfiguration { get; }

        RsaPrivateConfiguration? RsaPrivateConfiguration { get; }

        RsaPublicConfiguration? RsaPublicConfiguration { get; }
    }
}
