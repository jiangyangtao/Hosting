using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Encryption.AdvancedEncryptionStandard
{
    public abstract class AesBase
    {
        protected System.Security.Cryptography.Aes Aes;

        protected AesBase(string secretKey, string iv)
        {
            if (secretKey.IsNullOrEmpty()) throw new ArgumentNullException(nameof(secretKey));
            if (secretKey.Length < 16) throw new ArgumentOutOfRangeException(nameof(secretKey));
            if (secretKey.Length > 32) throw new ArgumentOutOfRangeException(nameof(secretKey));
            if (secretKey.Length != 16 && secretKey.Length != 24 && secretKey.Length != 32) throw new ArgumentOutOfRangeException(nameof(secretKey));

            if (iv.IsNullOrEmpty()) throw new ArgumentNullException(nameof(iv));
            if (iv.Length != 16) throw new ArgumentOutOfRangeException(nameof(iv));

            SecretKey = secretKey;
            Iv = iv;

            Aes = System.Security.Cryptography.Aes.Create();
            Aes.IV = Encoding.UTF8.GetBytes(Iv);
            Aes.Key = Encoding.UTF8.GetBytes(SecretKey);
        }

        public string SecretKey { get; }

        public string Iv { get; }

        public ICryptoTransform CryptoTransform => Aes.CreateEncryptor();

        #region 将要加密的字符串进行AES加密(CBC模式)
        /// <summary>
        /// 将要加密的字符串进行AES加密(CBC模式)
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <returns>密文</returns>
        /// <exception cref="ArgumentNullException">value 参数为空或者为 null</exception>
        public string Encrypt(string value) => Encrypt(value, CipherMode.CBC, PaddingMode.PKCS7);

        public string Encrypt(string value, CipherMode cipherMode, PaddingMode paddingMode)
        {
            if (value.IsNullOrEmpty()) throw new ArgumentNullException(nameof(value));

            Aes.Mode = cipherMode;
            Aes.Padding = paddingMode;

            var cryptoTransform = Aes.CreateEncryptor();
            return Handle(value, cryptoTransform);
        }
        #endregion

        #region 将要解密的字符串进行AES解密(CBC模式)
        /// <summary>
        /// 将要解密的字符串进行AES解密(CBC模式)
        /// </summary>
        /// <param name="value">要解密的字符串</param>
        /// <returns>
        /// 明文
        /// </returns>
        /// <exception cref="ArgumentNullException">value 参数为空或者为 null</exception>
        public string Decrypt(string value) => Decrypt(value, CipherMode.CBC, PaddingMode.PKCS7);

        public string Decrypt(string value, CipherMode cipherMode, PaddingMode paddingMode)
        {
            if (value.IsNullOrEmpty()) throw new ArgumentNullException(nameof(value));

            Aes.Mode = cipherMode;
            Aes.Padding = paddingMode;

            var cryptoTransform = Aes.CreateDecryptor();
            return Handle(value, cryptoTransform);
        }
        #endregion

        private static string Handle(string value, ICryptoTransform cryptoTransform)
        {
            var valueBytes = Convert.FromBase64String(value);
            var resultArray = cryptoTransform.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            return Convert.ToBase64String(resultArray);
        }
    }
}
