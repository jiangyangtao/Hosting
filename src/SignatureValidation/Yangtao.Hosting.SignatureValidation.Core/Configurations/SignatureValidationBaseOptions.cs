using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public abstract class SignatureValidationBaseOptions
    {
        public ValidationType ValidationType { set; get; }

        public SignatureAlgorithm SignatureAlgorithm { set; get; }

        public bool IsHmacShaSignature => ValidationType == ValidationType.Signatrue && SignatureAlgorithm == SignatureAlgorithm.HmacSha;

        public bool IsRsaSignature => ValidationType == ValidationType.Signatrue && SignatureAlgorithm == SignatureAlgorithm.RSA;

        public bool IsRsaEncryption => ValidationType == ValidationType.Encrypt;
    }
}
