namespace Yangtao.Hosting.GrpcCore.Options
{
    /// <summary>
    /// RSA 私钥配置
    /// </summary>
    public class RsaPrivateSignOptions : RsaSignOptions
    {
        /// <summary>
        /// RSA 私钥
        /// </summary>
        public string? PrivateKey { set; get; }
    }
}
