using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Encryption.TripleDataEncryptionAlgorithm
{
    public abstract class TripleDesBase
    {
        protected TripleDesBase(string secretKey, string iv)
        {
            if (secretKey.IsNullOrEmpty()) throw new ArgumentNullException(nameof(secretKey));
            if (secretKey.Length < 24) throw new ArgumentOutOfRangeException(nameof(secretKey));

            if (iv.IsNullOrEmpty()) throw new ArgumentNullException(nameof(iv));
            if (iv.Length < 8) throw new ArgumentOutOfRangeException(nameof(iv));

            SecretKey = secretKey;
            Iv = iv;
        }

        public string SecretKey { get; }

        public string Iv { get; }

        public byte[] SecretKeyBytes => Encoding.UTF8.GetBytes(SecretKey);

        public byte[] IvBytes => Encoding.UTF8.GetBytes(Iv);


        #region 将要加密的字符串进行3DES加密

        /// <summary>
        /// 将要加密的字符串进行3DES加密
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <returns>
        /// 如果 value 参数为 null 或者为空字符串("")，则返回 <see cref="string.Empty"/>；
        /// 否则返回3DES算法加密后的密文。
        /// </returns>
        public string Encrypt(string value)
        {
            if (value.IsNullOrEmpty()) return string.Empty;

            var valueBytes = Encoding.UTF8.GetBytes(value);
            using var tdes = TripleDES.Create();
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, tdes.CreateEncryptor(SecretKeyBytes, IvBytes), CryptoStreamMode.Write);
            cryptoStream.Write(valueBytes, 0, valueBytes.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();
            memoryStream.Close();

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        #endregion

        #region 将要解密的字符串进行3DES解密
        /// <summary>
        /// 将要解密的字符串进行3DES解密
        /// </summary>
        /// <param name="value">要解密的字符串</param>
        /// <returns>
        /// 如果 value 参数为 null 或者为空字符串("")，则返回 <see cref="string.Empty"/>；
        /// 否则返回3DES算法解密后的明文。
        /// </returns>
        public string Decrypt(string value)
        {
            if (value.IsNullOrEmpty()) return string.Empty;

            var valueBytes = Convert.FromBase64String(value);
            using var tdes = TripleDES.Create();
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, tdes.CreateDecryptor(SecretKeyBytes, IvBytes), CryptoStreamMode.Write);
            cryptoStream.Write(valueBytes, 0, valueBytes.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();
            memoryStream.Close();

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
        #endregion
    }
}
