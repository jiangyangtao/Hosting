using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaPrivate : RsaPrivateBase, IDisposable
    {
        public RsaPrivate(string privateKey) : base(privateKey, RSAKeyType.Pkcs1) { }

        public RsaPrivate(string privateKey, RSAKeyType keyType) : base(privateKey, keyType) { }

        public static RsaPublic Create(string privateKey) => new(privateKey);

        public static RsaPublic Create(string privateKey, RSAKeyType keyType) => new(privateKey, keyType);

        public void Dispose()
        {
            Rsa.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
