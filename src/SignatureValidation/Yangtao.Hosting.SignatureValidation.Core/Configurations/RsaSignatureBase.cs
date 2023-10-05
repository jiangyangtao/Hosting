using System.Security.Cryptography;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public abstract class RsaSignatureBase: RsaBase
    {
        public RSASignaturePaddingMode RSASignaturePaddingMode { set; get; }
    }
}
