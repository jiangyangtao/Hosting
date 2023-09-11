using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaPublicProvider : RsaPublicBase
    {
        public RsaPublicProvider(string publicKey) : base(publicKey, RSAKeyType.Pkcs1) { }

        public RsaPublicProvider(string publicKey, RSAKeyType keyType) : base(publicKey, keyType) { }
    }
}
