using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Client.Configurations
{
    internal class ClientSignatureValidationOptions : ClientEncryptionValidationOptions
    {
        public SignatureAlgorithm? SignatureAlgorithm { set; get; }

        public HmacShaOptions HmacShaOptions { set; get; }
    }
}
