﻿namespace Yangtao.Hosting.GrpcClient
{
    public class GrpcClientOptions
    {
        public string Endpoint { set; get; }

        /// <summary>
        /// // 允许自签名证书
        /// </summary>
        public bool AllowUnsafeCertificate { set; get; } = true;

        /// <summary>
        /// 重试策略
        /// </summary>
        public bool AddRetry { set; get; } = true;

        public bool UseAuthenticationGrpcClientInterceptor { set; get; } = true;
    }
}
