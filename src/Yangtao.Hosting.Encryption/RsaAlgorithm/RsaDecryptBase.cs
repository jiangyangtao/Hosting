using RSAExtensions;
using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaDecryptBase
    {
        protected readonly string PrivateKey;
        protected readonly RSAKeyType KeyType;
        protected readonly RSA Rsa;

        public RsaDecryptBase(string privateKey, RSAKeyType keyType = RSAKeyType.Pkcs1)
        {
            if (privateKey.IsNullOrEmpty()) throw new ArgumentNullException(nameof(privateKey));

            PrivateKey = privateKey;
            KeyType = keyType;

            Rsa = RSA.Create();
            Rsa.ImportPrivateKey(KeyType, PrivateKey);
        }

        public string Decrypt(string ciphertext) => Decrypt(ciphertext, RSAEncryptionPadding.Pkcs1);

        public string Decrypt(string ciphertext, RSAEncryptionPadding padding)
        {
            if (ciphertext.IsNullOrEmpty()) throw new ArgumentNullException(nameof(ciphertext));

            var bytes = Encoding.UTF8.GetBytes(ciphertext);
            var result = Rsa.Decrypt(bytes, padding);
            return Encoding.UTF8.GetString(result);
        }

        public string DecryptBigData(string ciphertext) => DecryptBigData(ciphertext, RSAEncryptionPadding.Pkcs1);

        public string DecryptBigData(string ciphertext, RSAEncryptionPadding padding) => Rsa.DecryptBigData(ciphertext, padding);
    }
}
