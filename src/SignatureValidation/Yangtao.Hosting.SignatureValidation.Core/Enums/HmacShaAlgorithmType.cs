
namespace Yangtao.Hosting.SignatureValidation.Core.Enums
{
    public enum HmacShaAlgorithmType
    {
        /// <summary>
        /// HS256
        /// </summary>
        HmacSha256 = 1,

        /// <summary>
        /// HS384
        /// </summary>
        HmacSha384 = 2,

        /// <summary>
        /// HS512
        /// </summary>
        HmacSha512 = 3,
    }

    public enum HmacShaSignatureFormatType
    {
        Hexadecimal,

        Base64,
    }
}
