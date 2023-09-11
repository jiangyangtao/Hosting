using RSAExtensions;
using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Encryption.RsaAlgorithm
{
    public class RsaPrivateBase
    {
        protected readonly string PrivateKey;
        protected readonly RSAKeyType KeyType;
        protected readonly RSA Rsa;

        public RsaPrivateBase(string privateKey, RSAKeyType keyType = RSAKeyType.Pkcs1)
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

        public string SignData(string value) => SignData(value, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        public string SignData(string value, RSASignaturePadding signaturePadding) => SignData(value, HashAlgorithmName.SHA256, signaturePadding);

        public string SignData(string value, HashAlgorithmName algorithmName, RSASignaturePadding signaturePadding)
        {
            if (value.IsNullOrEmpty()) throw new ArgumentNullException(nameof(value));

            var format = new RSAPKCS1SignatureFormatter();
            var bytes = Encoding.UTF8.GetBytes(value);
            var result = Rsa.SignData(bytes, algorithmName, signaturePadding);
            return Convert.ToBase64String(result);
        }

        public string DecryptBigData(string ciphertext) => DecryptBigData(ciphertext, RSAEncryptionPadding.Pkcs1);

        public string DecryptBigData(string ciphertext, RSAEncryptionPadding padding) => Rsa.DecryptBigData(ciphertext, padding);
    }
}
