namespace Yangtao.Hosting.GrpcCore.Options
{
    /// <summary>
    /// 签名验证配置
    /// </summary>
    public class SignAuthenticationOptions
    {
        /// <summary>
        /// 签名验证类型
        /// </summary>
        public SignAuthenticationType? SignAuthenticationType { set; get; }

        /// <summary>
        /// AES 签名配置
        /// </summary>
        public AesSignOptions? AesSignOptions { set; get; }

        /// <summary>
        /// RSA 私钥签名配置
        /// </summary>
        public RsaPrivateSignOptions? RsaPrivateSignOptions { set; get; }

        /// <summary>
        /// RSA 公钥签名配置
        /// </summary>
        public RsaPublicSignOptions? RsaPublicSignOptions { set; get; }
    }
}
