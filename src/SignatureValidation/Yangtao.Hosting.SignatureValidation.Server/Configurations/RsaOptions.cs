using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server.Configurations
{
    public class RsaOptions : RsaBase
    {
        public string PrivateKey { set; get; }
    }
}
