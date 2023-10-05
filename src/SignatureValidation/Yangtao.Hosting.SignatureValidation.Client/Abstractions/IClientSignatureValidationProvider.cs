namespace Yangtao.Hosting.SignatureValidation.Client.Abstractions
{
    public interface IClientSignatureValidationProvider
    {     
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="value"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        bool VerifyData(string value, string signature);
    }
}
