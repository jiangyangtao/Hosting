using Grpc.Net.ClientFactory;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcClient.Options
{
    public class GrpcClientOptions
    {
        private readonly ICollection<string> _signAuthenticationMethods;

        public GrpcClientOptions()
        {
            _signAuthenticationMethods = new List<string>();
        }

        public string? Endpoint { set; get; }

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

        public SignAuthenticationType? SignAuthenticationType { set; get; }

        public AesSignOptions? AesSignOptions { set; get; }

        public RsaPublicSignOptions? RsaPublicSignOptions { set; get; }

        /// <summary>
        /// 签名校验的方法集，为空则全部都需要校验
        /// </summary>
        public ICollection<string> SignAuthenticationMethods => _signAuthenticationMethods;

        internal bool ExisySignAuthMethods(string method)
        {
            if (_signAuthenticationMethods.Count == 0) return true;

            return _signAuthenticationMethods.Contains(method, StringComparison.OrdinalIgnoreCase);
        }
    }
}
