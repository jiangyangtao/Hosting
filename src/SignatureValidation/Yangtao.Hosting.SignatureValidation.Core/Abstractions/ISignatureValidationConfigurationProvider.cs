using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Abstractions
{
    public interface ISignatureValidationConfigurationProvider
    {
        public ValidationType ValidationType { set; get; }
    }
}
