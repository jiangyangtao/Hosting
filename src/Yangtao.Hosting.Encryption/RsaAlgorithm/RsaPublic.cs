using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaPublic : RsaPublicBase, IDisposable
    {
        public RsaPublic(string publicKey) : base(publicKey, RSAKeyType.Pkcs1) { }

        public RsaPublic(string publicKey, RSAKeyType keyType = RSAKeyType.Pkcs1) : base(publicKey, keyType) { }

        public static RsaPublic Create(string publicKey) => new(publicKey);

        public static RsaPublic Create(string publicKey, RSAKeyType keyType) => new(publicKey, keyType);

        public void Dispose()
        {
            Rsa.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
