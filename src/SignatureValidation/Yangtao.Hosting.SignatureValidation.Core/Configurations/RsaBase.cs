using RSAExtensions;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public abstract class RsaBase
    {
        public RSAKeyType RSAKeyType { set; get; }

        public RSAEncryptionPaddingType RSAEncryptionPaddingType { set; get; }
    }
}
