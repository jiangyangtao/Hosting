namespace Yangtao.Hosting.SignatureValidation.Client.Abstractions
{
    public interface IClientSignatureValidationProvider
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        string Encrypt(string plaintext);

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="value"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        bool VerifyData(string value, string signature);
    }
}
