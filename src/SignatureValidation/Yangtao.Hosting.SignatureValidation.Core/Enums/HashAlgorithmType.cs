
namespace Yangtao.Hosting.SignatureValidation.Core.Enums
{
    public enum HashAlgorithmType
    {
        /// <summary>
        /// HS256
        /// </summary>
        SHA256 = 1,

        /// <summary>
        /// HS384
        /// </summary>
        SHA384 = 2,

        /// <summary>
        /// HS512
        /// </summary>
        SHA512 = 3,
    }

    public enum HmacShaSignatureFormatType
    {
        Hexadecimal,

        Base64,
    }
}
