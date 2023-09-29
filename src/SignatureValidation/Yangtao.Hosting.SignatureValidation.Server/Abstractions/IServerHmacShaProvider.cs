namespace Yangtao.Hosting.SignatureValidation.Server.Abstractions
{
    internal interface IServerHmacShaProvider
    {
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string SignData(string value);
    }
}
