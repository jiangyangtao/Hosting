using RSAExtensions;
using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public abstract class RsaEncryptBase
    {
        protected readonly string PrivateKey;
        protected readonly RSAKeyType KeyType = RSAKeyType.Pkcs1;
        protected readonly RSA Rsa = RSA.Create();

        public RsaEncryptBase(string privateKey, RSAKeyType keyType = RSAKeyType.Pkcs1)
        {
            PrivateKey = privateKey;
            KeyType = keyType;
            Rsa.ImportPrivateKey(KeyType, PrivateKey);
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
