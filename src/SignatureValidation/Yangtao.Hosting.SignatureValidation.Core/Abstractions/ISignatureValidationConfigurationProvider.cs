using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Abstractions
{
    public interface ISignatureValidationConfigurationProvider
    {
        public ValidationType ValidationType { set; get; }

        public HmacShaConfiguration HmacShaConfiguration { set; get; }

        public RsaPrivateConfiguration RsaPrivateConfiguration { set; get; }

        public RsaPublicConfiguration RsaPublicConfiguration { set; get; }
    }
}
