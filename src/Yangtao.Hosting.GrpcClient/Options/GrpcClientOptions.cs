﻿using Grpc.Net.ClientFactory;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcClient.Options
{
    public class GrpcClientOptions : SignAuthenticationOptions
    {
        internal GrpcClientOptions() { }

        public string Endpoint { set; get; }

        /// <summary>
        /// 允许自签名证书
        /// </summary>
        public bool AllowAnyServerCertificate { set; get; } = true;

        /// <summary>
        /// 重试策略
        /// </summary>
        public bool AddRetry { set; get; } = true;

        public bool UseAuthenticationGrpcClientInterceptor { set; get; } = true;

        public InterceptorScope InterceptorScope { set; get; } = InterceptorScope.Channel;
    }
}
