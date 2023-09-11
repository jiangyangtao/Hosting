using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaPrivateProvider : RsaPrivateBase
    {
        public RsaPrivateProvider(string privateKey) : base(privateKey, RSAKeyType.Pkcs1) { }

        public RsaPrivateProvider(string privateKey, RSAKeyType keyType) : base(privateKey, keyType) { }
    }
}
