using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaEncryptProvider : RsaEncryptBase
    {
        public RsaEncryptProvider(string publicKey) : base(publicKey, RSAKeyType.Pkcs1) { }

        public RsaEncryptProvider(string publicKey, RSAKeyType keyType) : base(publicKey, keyType) { }
    }
}
