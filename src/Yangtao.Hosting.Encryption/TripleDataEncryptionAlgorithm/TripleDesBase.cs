using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Encryption.TripleDataEncryptionAlgorithm
{
    public abstract class TripleDesBase
    {
        protected readonly TripleDES TripleDES;

        protected TripleDesBase(string secretKey, string iv)
        {
            if (secretKey.IsNullOrEmpty()) throw new ArgumentNullException(nameof(secretKey));
            if (secretKey.Length < 24) throw new ArgumentOutOfRangeException(nameof(secretKey));

            if (iv.IsNullOrEmpty()) throw new ArgumentNullException(nameof(iv));
            if (iv.Length < 8) throw new ArgumentOutOfRangeException(nameof(iv));

            SecretKey = secretKey;
            Iv = iv;

            TripleDES = TripleDES.Create();
        }

        public string SecretKey { get; }

        public string Iv { get; }

        public byte[] SecretKeyBytes => Encoding.UTF8.GetBytes(SecretKey);

        public byte[] IvBytes => Encoding.UTF8.GetBytes(Iv);

        protected ICryptoTransform ICryptoTransform => TripleDES.CreateEncryptor(SecretKeyBytes, IvBytes);

        /// <summary>
        /// 将要加密的字符串进行加密
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <returns>
        /// 密文
        /// </returns>
        /// <exception cref="ArgumentNullException">value 参数为空或者为 null</exception>
        public string Encrypt(string value)
        {
            if (value.IsNullOrEmpty()) throw new ArgumentNullException(nameof(value));

            var cryptoTransform = TripleDES.CreateEncryptor(SecretKeyBytes, IvBytes);
            return Handle(value, cryptoTransform);
        }


        /// <summary>
        /// 将要解密的字符串进行解密
        /// </summary>
        /// <param name="value">要解密的字符串</param>
        /// <returns>
        /// 明文
        /// </returns>
        /// <exception cref="ArgumentNullException">value 参数为空或者为 null</exception>
        public string Decrypt(string value)
        {
            if (value.IsNullOrEmpty()) throw new ArgumentNullException(nameof(value));

            var cryptoTransform = TripleDES.CreateDecryptor(SecretKeyBytes, IvBytes);

            return Handle(value, cryptoTransform);
        }

        private static string Handle(string value, ICryptoTransform cryptoTransform)
        {
            var valueBytes = Convert.FromBase64String(value);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
            cryptoStream.Write(valueBytes, 0, valueBytes.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();
            memoryStream.Close();

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
