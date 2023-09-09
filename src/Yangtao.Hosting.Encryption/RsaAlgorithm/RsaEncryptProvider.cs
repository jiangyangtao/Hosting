using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaEncryptProvider : RsaEncryptBase
    {
        public RsaEncryptProvider(string privateKey) : base(privateKey, RSAKeyType.Pkcs1) { }

        public RsaEncryptProvider(string privateKey, RSAKeyType keyType) : base(privateKey, keyType) { }
    }
}
