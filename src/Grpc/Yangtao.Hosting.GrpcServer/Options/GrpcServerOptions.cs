using Grpc.AspNetCore.Server;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcServer.Options
{
    /// <summary>
    /// Grpc Server 配置
    /// </summary>
    public class GrpcServerOptions
    {
        internal GrpcServerOptions()
        {
            GrpcServiceOptions = new GrpcServiceOptions();
        }

        /// <summary>
        /// 原生 <see cref="GrpcServiceOptions"/> 配置
        /// </summary>
        public GrpcServiceOptions GrpcServiceOptions { get; }

        /// <summary>
        /// 签名验证类型
        /// </summary>
        public SignAuthenticationType? SignAuthenticationType { set; get; }

        /// <summary>
        /// AES 签名配置
        /// </summary>
        public AesSignOptions? AesSignOptions { set; get; }

        /// <summary>
        /// RSA 签名配置
        /// </summary>
        public RsaPrivateSignOptions? RsaPrivateSignOptions { set; get; }
    }
}
