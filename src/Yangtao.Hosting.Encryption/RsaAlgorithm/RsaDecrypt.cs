using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaDecrypt : RsaDecryptBase, IDisposable
    {
        public RsaDecrypt(string publicKey) : base(publicKey, RSAKeyType.Pkcs1) { }

        public RsaDecrypt(string publicKey, RSAKeyType keyType) : base(publicKey, keyType) { }

        public static RsaEncrypt Create(string publicKey) => new(publicKey);

        public static RsaEncrypt Create(string publicKey, RSAKeyType keyType) => new(publicKey, keyType);

        public void Dispose()
        {
            Rsa.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
