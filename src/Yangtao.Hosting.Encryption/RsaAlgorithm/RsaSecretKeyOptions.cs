using RSAExtensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaSecretKeyOptions
    {
        internal RsaSecretKeyOptions() { }

        public string SecretKey { set; get; }

        public RSAKeyType RSAKeyType { set; get; } = RSAKeyType.Pkcs1;
    }
}
