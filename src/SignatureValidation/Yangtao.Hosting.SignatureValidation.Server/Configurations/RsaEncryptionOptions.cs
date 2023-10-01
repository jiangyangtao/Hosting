using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server.Configurations
{
    public class RsaEncryptionOptions : RsaEncryptionBase
    {
        public string PublicKey { set; get; }
    }
}
