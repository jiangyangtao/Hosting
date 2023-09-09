
namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaSecretKey
    {
        internal RsaSecretKey() { }

        public string PublicKey { get; internal set; }

        public string PrivateKey { get; internal set; }
    }
}
