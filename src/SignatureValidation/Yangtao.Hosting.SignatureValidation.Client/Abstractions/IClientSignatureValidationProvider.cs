namespace Yangtao.Hosting.SignatureValidation.Client.Abstractions
{
    public interface IClientSignatureValidationProvider
    {
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
