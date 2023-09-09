using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaDecryptProvider : RsaDecryptBase
    {
        public RsaDecryptProvider(string privateKey) : base(privateKey, RSAKeyType.Pkcs1) { }

        public RsaDecryptProvider(string privateKey, RSAKeyType keyType) : base(privateKey, keyType) { }
    }
}
