using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Encryption.AdvancedEncryptionStandard
{
    public abstract class AesBase
    {
        protected readonly System.Security.Cryptography.Aes Aes;

        protected AesBase(string securityKey, string iv)
        {
            if (securityKey.IsNullOrEmpty()) throw new ArgumentNullException(nameof(securityKey));
            if (securityKey.Length != 16 &&
                securityKey.Length != 24 &&
                securityKey.Length != 32 &&
                securityKey.Length != 40 &&
                securityKey.Length != 48) throw new ArgumentOutOfRangeException(nameof(securityKey));

            if (iv.IsNullOrEmpty()) throw new ArgumentNullException(nameof(iv));
            if (iv.Length != 16) throw new ArgumentOutOfRangeException(nameof(iv));

            SecurityKey = securityKey;
            Iv = iv;

            Aes = System.Security.Cryptography.Aes.Create();
            Aes.IV = Encoding.UTF8.GetBytes(Iv);
            Aes.Key = Encoding.UTF8.GetBytes(SecurityKey);
        }

        public string SecurityKey { get; }

        public string Iv { get; }

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

            var bytes = Encoding.UTF8.GetBytes(value);
            var cryptoTransform = Aes.CreateEncryptor();
            var resultArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);

            return Convert.ToBase64String(resultArray);
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

            var valueBytes = Convert.FromBase64String(value);
            var cryptoTransform = Aes.CreateDecryptor();
            var resultArray = cryptoTransform.TransformFinalBlock(valueBytes, 0, valueBytes.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
        #endregion
    }
}
