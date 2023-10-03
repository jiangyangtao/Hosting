using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public class HmacShaConfiguration
    {
        public HmacShaAlgorithmType HmacShaAlgorithmType { set; get; }

        public string SecretKey { set; get; }
    }
}
