using Grpc.Net.ClientFactory;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcClient.Options
{
    /// <summary>
    /// Grpc Client 配置
    /// </summary>
    public class GrpcClientOptions
    {
        private readonly ICollection<string> _signAuthenticationMethods;

        /// <summary>
        /// 构建 <see cref="GrpcClientOptions"/>
        /// </summary>
        public GrpcClientOptions()
        {
            _signAuthenticationMethods = new List<string>();
        }

        /// <summary>
        /// 接入点
        /// </summary>
        public string? Endpoint { set; get; }

        /// <summary>
        /// 允许自签名证书，默认允许
        /// </summary>
        public bool AllowAnyServerCertificate { set; get; } = true;

        /// <summary>
        /// 允许重试策略，默认允许
        /// </summary>
        public bool AllowRetry { set; get; } = true;

        /// <summary>
        /// 是否使用与项目一致的验证方式，默认使用。
        /// 如果使用签名验证则设置为 false
        /// </summary>
        public bool UseAuthenticationGrpcClientInterceptor { set; get; } = true;

        /// <summary>
        /// 拦截器范围，默认 Channel
        /// </summary>
        public InterceptorScope InterceptorScope { set; get; } = InterceptorScope.Channel;

        /// <summary>
        /// 签名验证类型
        /// </summary>
        public SignAuthenticationType? SignAuthenticationType { set; get; }

        /// <summary>
        /// AES 签名配置
        /// </summary>
        public AesSignOptions? AesSignOptions { set; get; }

        /// <summary>
        /// RSA 公钥签名配置
        /// </summary>
        public RsaPublicSignOptions? RsaPublicSignOptions { set; get; }

        /// <summary>
        /// 签名校验的方法集，为空则全部都需要校验
        /// </summary>
        public ICollection<string> SignAuthenticationMethods => _signAuthenticationMethods;
    }
}
