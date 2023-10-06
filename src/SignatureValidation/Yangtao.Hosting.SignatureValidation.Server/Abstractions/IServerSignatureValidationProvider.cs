namespace Yangtao.Hosting.SignatureValidation.Server.Abstractions
{
    public interface IServerSignatureValidationProvider
    {
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string SignData(string value);
    }
}
