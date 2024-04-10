using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RSAExtensions;
using System.Security.Authentication;

namespace Yangtao.Hosting.GrpcCore.Options
{
    internal class RsaSignOptions
    {
        public HashAlgorithmType AlgorithmType { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RSAKeyType RSAKeyType { set; get; }
    }
}
