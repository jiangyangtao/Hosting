using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public class SignatureValidationConfiguration
    {
        public ValidationType ValidationType { set; get; }

        public SignatureAlgorithm SignatureAlgorithm { set; get; }
    }
}
