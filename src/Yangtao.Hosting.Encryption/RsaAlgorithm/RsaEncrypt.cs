using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaEncrypt : RsaEncryptBase, IDisposable
    {
        public RsaEncrypt(string privateKey) : base(privateKey, RSAKeyType.Pkcs1) { }

        public RsaEncrypt(string privateKey, RSAKeyType keyType = RSAKeyType.Pkcs1) : base(privateKey, keyType) { }

        public void Dispose()
        {
            Rsa.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
