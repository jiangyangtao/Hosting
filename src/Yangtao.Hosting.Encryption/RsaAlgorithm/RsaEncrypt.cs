using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaEncrypt : RsaEncryptBase, IDisposable
    {
        public RsaEncrypt(string privateKey) : base(privateKey, RSAKeyType.Pkcs1) { }

        public RsaEncrypt(string privateKey, RSAKeyType keyType = RSAKeyType.Pkcs1) : base(privateKey, keyType) { }

        public static RsaEncrypt Create(string privateKey) => new(privateKey);

        public static RsaEncrypt Create(string privateKey, RSAKeyType keyType) => new(privateKey, keyType);

        public void Dispose()
        {
            Rsa.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
