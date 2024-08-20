namespace Yangtao.Hosting.GrpcCore.Options
{
    /// <summary>
    /// RSA 公钥配置
    /// </summary>
    public class RsaPublicSignOptions : RsaSignOptions
    {
        /// <summary>
        /// RSA 公钥
        /// </summary>
        public string? PublicKey { set; get; }
    }
}
