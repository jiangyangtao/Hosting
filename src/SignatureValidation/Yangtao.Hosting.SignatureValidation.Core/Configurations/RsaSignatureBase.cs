using System.Security.Cryptography;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public abstract class RsaSignatureBase : RsaBase
    {
        public RSASignaturePaddingMode RSASignaturePaddingMode { set; get; }

        public RSASignaturePadding RSASignaturePadding
        {
            get
            {
                if (RSASignaturePaddingMode == RSASignaturePaddingMode.Pkcs1) return RSASignaturePadding.Pkcs1;

                return RSASignaturePadding.Pss;
            }
        }
    }
}
