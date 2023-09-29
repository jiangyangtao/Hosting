namespace Yangtao.Hosting.SignatureValidation.Server.Abstractions
{
    public interface IServerSignatureValidationProvider
    {
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
    }
}
