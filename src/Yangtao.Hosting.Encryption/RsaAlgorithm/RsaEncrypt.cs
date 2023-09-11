using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaEncrypt : RsaEncryptBase, IDisposable
    {
        public RsaEncrypt(string publicKey) : base(publicKey, RSAKeyType.Pkcs1) { }

        public RsaEncrypt(string publicKey, RSAKeyType keyType = RSAKeyType.Pkcs1) : base(publicKey, keyType) { }

        public static RsaEncrypt Create(string publicKey) => new(publicKey);

        public static RsaEncrypt Create(string publicKey, RSAKeyType keyType) => new(publicKey, keyType);

        public void Dispose()
        {
            Rsa.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
