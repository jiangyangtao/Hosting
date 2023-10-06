using System.Security.Cryptography;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public abstract class RsaSignatureBase : RsaBase
    {
        public RSASignaturePaddingMode RSASignaturePaddingMode { set; get; } = RSASignaturePaddingMode.Pkcs1;

        public HashAlgorithmType HashAlgorithmType { set; get; } = HashAlgorithmType.SHA256;

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
                if (HashAlgorithmType == HashAlgorithmType.SHA256) return HashAlgorithmName.SHA256;
                if (HashAlgorithmType == HashAlgorithmType.SHA384) return HashAlgorithmName.SHA384;

                return HashAlgorithmName.SHA512;
            }
        }
    }
}
