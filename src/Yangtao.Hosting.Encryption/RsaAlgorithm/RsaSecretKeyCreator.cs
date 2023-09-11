using RSAExtensions;
using System.Security.Cryptography;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaSecretKeyCreator
    {
        internal RsaSecretKeyCreator()
        {
        }

        public static RsaSecretKey Generate(RSAKeyType keyType = RSAKeyType.Pkcs1)
        {
            using var rsa = RSA.Create();

            var privateKey = rsa.ExportPrivateKey(keyType);
            var publicKey = rsa.ExportPublicKey(keyType);
            return new RsaSecretKey
            {
                PrivateKey = privateKey,
                PublicKey = publicKey,
            };
        }
    }
}
