using RSAExtensions;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public abstract class RsaBase
    {
        public RSAKeyType RSAKeyType { set; get; } = RSAKeyType.Pkcs8;
    }
}
