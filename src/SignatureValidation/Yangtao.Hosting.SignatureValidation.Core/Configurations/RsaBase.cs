using RSAExtensions;
using System.Security.Cryptography;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public abstract class RsaBase
    {
        public RSAKeyType RSAKeyType { set; get; } = RSAKeyType.Pkcs8;

        public RSAEncryptionPaddingType RSAEncryptionPaddingType { set; get; } = RSAEncryptionPaddingType.Pkcs1;

        public RSAEncryptionPadding RSAEncryptionPadding
        {
            get
            {
                if (RSAEncryptionPaddingType == RSAEncryptionPaddingType.Pkcs1) return RSAEncryptionPadding.Pkcs1;
                if (RSAEncryptionPaddingType == RSAEncryptionPaddingType.OaepSHA1) return RSAEncryptionPadding.OaepSHA1;
                if (RSAEncryptionPaddingType == RSAEncryptionPaddingType.OaepSHA256) return RSAEncryptionPadding.OaepSHA256;
                if (RSAEncryptionPaddingType == RSAEncryptionPaddingType.OaepSHA384) return RSAEncryptionPadding.OaepSHA384;

                return RSAEncryptionPadding.OaepSHA512;
            }
        }
    }
}
