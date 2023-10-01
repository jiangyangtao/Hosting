using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server.Configurations
{
    public class RsaSignatureOptions : RsaSignatureBase
    {
        public string PublicKey { set; get; }
    }
}
