using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Client.Configurations
{
    public abstract class RsaPublicConfiguration : RsaBase
    {
        public string PublicKey { set; get; }
    }
}
