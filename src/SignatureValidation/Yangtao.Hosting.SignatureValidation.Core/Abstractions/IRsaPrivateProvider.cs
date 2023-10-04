
namespace Yangtao.Hosting.SignatureValidation.Core.Abstractions
{
    internal interface IRsaPrivateProvider
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
