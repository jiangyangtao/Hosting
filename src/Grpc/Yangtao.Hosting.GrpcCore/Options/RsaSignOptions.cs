using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RSAExtensions;

namespace Yangtao.Hosting.GrpcCore.Options
{
    /// <summary>
    /// RSA 签名配置基类
    /// </summary>
    public abstract class RsaSignOptions
    {
        /// <summary>
        /// RSA 密钥类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public RSAKeyType RSAKeyType { set; get; }
    }
}
