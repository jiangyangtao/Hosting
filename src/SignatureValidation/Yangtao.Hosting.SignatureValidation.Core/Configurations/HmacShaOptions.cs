using System.Text;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public class HmacShaOptions
    {
        public HmacShaAlgorithmType HmacShaAlgorithmType { set; get; } = HmacShaAlgorithmType.HmacSha256;

        public HmacShaSignatureFormatType HmacShaSignatureFormatType { set; get; } = HmacShaSignatureFormatType.Base64;

        public string SecretKey { set; get; }

        public byte[] SecretKeyBytes => Encoding.UTF8.GetBytes(SecretKey);
    }
}
