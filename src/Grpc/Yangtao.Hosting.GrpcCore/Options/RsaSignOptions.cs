using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RSAExtensions;

namespace Yangtao.Hosting.GrpcCore.Options
{
    public abstract class RsaSignOptions
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public RSAKeyType RSAKeyType { set; get; }
    }
}
