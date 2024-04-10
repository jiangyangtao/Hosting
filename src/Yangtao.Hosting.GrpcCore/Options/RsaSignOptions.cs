using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RSAExtensions;
using System.Security.Authentication;

namespace Yangtao.Hosting.GrpcCore.Options
{
    public abstract class RsaSignOptions
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public HashAlgorithmType AlgorithmType { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RSAKeyType RSAKeyType { set; get; }
    }
}
