using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaDecrypt : RsaDecryptBase, IDisposable
    {
        public RsaDecrypt(string privateKey) : base(privateKey, RSAKeyType.Pkcs1) { }

        public RsaDecrypt(string privateKey, RSAKeyType keyType) : base(privateKey, keyType) { }

        public static RsaEncrypt Create(string privateKey) => new(privateKey);

        public static RsaEncrypt Create(string privateKey, RSAKeyType keyType) => new(privateKey, keyType);

        public void Dispose()
        {
            Rsa.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
