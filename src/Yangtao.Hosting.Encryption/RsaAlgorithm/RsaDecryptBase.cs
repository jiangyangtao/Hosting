using RSAExtensions;
using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaDecryptBase
    {
        protected readonly string PublicKey;
        protected readonly RSAKeyType KeyType = RSAKeyType.Pkcs1;
        protected readonly RSA Rsa = RSA.Create();

        public RsaDecryptBase(string privateKey, RSAKeyType keyType = RSAKeyType.Pkcs1)
        {
            PublicKey = privateKey;
            KeyType = keyType;
            Rsa.ImportPublicKey(KeyType, PublicKey);
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
