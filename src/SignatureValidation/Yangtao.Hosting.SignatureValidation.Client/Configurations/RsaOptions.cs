using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Client.Configurations
{
    public abstract class RsaOptions : RsaBase
    {
        public string PublicKey { set; get; }
    }
}
