using System.Text;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public class HmacShaOptions
    {
        public HashAlgorithmType HmacShaAlgorithmType { set; get; } = HashAlgorithmType.SHA256;

        public HmacShaSignatureFormatType HmacShaSignatureFormatType { set; get; } = HmacShaSignatureFormatType.Base64;

        public string SecretKey { set; get; }

        internal byte[] SecretKeyBytes => Encoding.UTF8.GetBytes(SecretKey);
    }
}
