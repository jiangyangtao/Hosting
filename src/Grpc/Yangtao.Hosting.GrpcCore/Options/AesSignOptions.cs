namespace Yangtao.Hosting.GrpcCore.Options
{
    /// <summary>
    /// AES 签名配置
    /// </summary>
    public class AesSignOptions
    {
        /// <summary>
        /// 向量
        /// </summary>
        public string? Iv { set; get; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string? SecurityKey { set; get; }
    }
}
