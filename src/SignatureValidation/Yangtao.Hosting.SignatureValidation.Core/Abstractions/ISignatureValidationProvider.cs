namespace Yangtao.Hosting.SignatureValidation.Core.Abstractions
{
    public interface ISignatureValidationProvider
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        string Encrypt(string plaintext);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        string Decrypt(string ciphertext);

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string SignData(string value);

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="value"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        bool VerifyData(string value, string signature);
    }
}
