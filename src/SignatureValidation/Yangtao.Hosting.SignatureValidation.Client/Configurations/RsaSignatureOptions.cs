using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Client.Configurations
{
    internal class RsaSignatureOptions : RsaSignatureBase
    {
        public string PublicKey { set; get; }
    }
}
