using RSAExtensions;
using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public abstract class RsaPublicBase
    {
        protected readonly string PublicKey;
        protected readonly RSAKeyType KeyType;
        protected readonly RSA Rsa;

        public RsaPublicBase(string publicKey, RSAKeyType keyType = RSAKeyType.Pkcs1)
        {
            if (publicKey.IsNullOrEmpty()) throw new ArgumentNullException(nameof(publicKey));

            PublicKey = publicKey;
            KeyType = keyType;

            Rsa = RSA.Create();
            Rsa.ImportPublicKey(KeyType, PublicKey);
        }

        public string Encrypt(string plaintext) => Encrypt(plaintext, RSAEncryptionPadding.Pkcs1);

        public string Encrypt(string plaintext, RSAEncryptionPadding padding)
        {
            if (plaintext.IsNullOrEmpty()) throw new ArgumentNullException(nameof(plaintext));

            var bytes = Encoding.UTF8.GetBytes(plaintext);
            var result = Rsa.Encrypt(bytes, padding);
            return Encoding.UTF8.GetString(result);
        }

        public string EncryptBigData(string plaintext) => EncryptBigData(plaintext, RSAEncryptionPadding.Pkcs1);

        public string EncryptBigData(string plaintext, RSAEncryptionPadding padding) => Rsa.EncryptBigData(plaintext, padding);
    }
}
