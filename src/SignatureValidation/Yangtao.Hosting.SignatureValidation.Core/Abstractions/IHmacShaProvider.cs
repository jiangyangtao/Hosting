using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Abstractions
{
    internal interface IHmacShaProvider
    {
        HmacShaAlgorithmType HmacShaAlgorithmType { get; }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="valueBytes"></param>
        /// <returns></returns>
        string SignData(byte[] valueBytes);

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="valueBytes"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        bool VerifyData(byte[] valueBytes, string signature);
    }
}
