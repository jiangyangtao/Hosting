using System.Security.Authentication;
using System.Security.Cryptography;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public abstract class RsaSignatureBase : RsaBase
    {
        public RSASignaturePaddingMode RSASignaturePaddingMode { set; get; } = RSASignaturePaddingMode.Pkcs1;

        public HashAlgorithmType HashAlgorithmType { set; get; } = HashAlgorithmType.Sha256;

        public RSASignaturePadding RSASignaturePadding
        {
            get
            {
                if (RSASignaturePaddingMode == RSASignaturePaddingMode.Pkcs1) return RSASignaturePadding.Pkcs1;

                return RSASignaturePadding.Pss;
            }
        }


        public HashAlgorithmName HashAlgorithmName
        {
            get
            {
                if (HashAlgorithmType == HashAlgorithmType.Sha256) return HashAlgorithmName.SHA256;
                if (HashAlgorithmType == HashAlgorithmType.Sha256) return HashAlgorithmName.SHA384;

                return HashAlgorithmName.SHA512;
            }
        }
    }
}
