﻿namespace Yangtao.Hosting.SignatureValidation.Server.Abstractions
{
    internal interface IServerHmacShaProvider
    {
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
