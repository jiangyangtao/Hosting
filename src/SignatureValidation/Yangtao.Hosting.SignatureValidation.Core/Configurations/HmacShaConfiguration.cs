using System.Text;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.Configurations
{
    public class HmacShaConfiguration
    {
        public HmacShaAlgorithmType HmacShaAlgorithmType { set; get; }

        public HmacShaSignatureFormatType HmacShaSignatureFormatType { set; get; }

        public string SecretKey { set; get; }

        public byte[] SecretKeyBytes => Encoding.UTF8.GetBytes(SecretKey);
    }
}
