using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Encryption.AdvancedEncryptionStandard
{
    public abstract class AesBase
    {
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
        }

        public string SecretKey { get; }

        public string Iv { get; }

        public byte[] SecretKeyBytes => Encoding.UTF8.GetBytes(SecretKey);

        public byte[] IvBytes => Encoding.UTF8.GetBytes(Iv);

        #region 将要加密的字符串进行AES加密(CBC模式)
        /// <summary>
        /// 将要加密的字符串进行AES加密(CBC模式)
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        public string Encrypt(string value)
        {
            if (value.IsNullOrEmpty()) return string.Empty;

            var valueBytes = Encoding.UTF8.GetBytes(value);

            using var aes = System.Security.Cryptography.Aes.Create();
            aes.IV = IvBytes;
            aes.Key = SecretKeyBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            var cryptoTransform = aes.CreateEncryptor();
            var resultArray = cryptoTransform.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        #endregion

        #region 将要解密的字符串进行AES解密(CBC模式)
        /// <summary>
        /// 将要解密的字符串进行AES解密(CBC模式)
        /// </summary>
        /// <param name="value">要解密的字符串</param>
        /// <returns>
        /// 如果 value 参数为 null 或者为空字符串("")，则返回 <see cref="string.Empty"/>；
        /// 否则返回AES算法解密后的明文。
        /// </returns>
        public string Decrypt(string value)
        {
            if (value.IsNullOrEmpty()) return string.Empty;

            var valueBytes = Convert.FromBase64String(value);
            using var aes = System.Security.Cryptography.Aes.Create();
            aes.IV = IvBytes;
            aes.Key = SecretKeyBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            var cryptoTransform = aes.CreateDecryptor();
            var resultArray = cryptoTransform.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
        #endregion
    }
}
