using System.Text;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.HmacShaPorviders
{
    internal abstract class HmacShaProviderBase
    {
        protected readonly HmacShaConfiguration _hmacShaConfiguration;

        protected HmacShaProviderBase(ISignatureValidationConfigurationProvider configurationProvider)
        {
            if (configurationProvider.IsHmacShaSignature && configurationProvider.HmacShaConfiguration == null)
                throw new NullReferenceException(nameof(HmacShaConfiguration));

            _hmacShaConfiguration = configurationProvider.HmacShaConfiguration;
        }

        protected string FormatSignature(byte[] resultBytes)
        {
            if (_hmacShaConfiguration.HmacShaSignatureFormatType == HmacShaSignatureFormatType.Base64)
                return Convert.ToBase64String(resultBytes);

            return ToHexadecimalFormat(resultBytes);
        }

        protected static string ToHexadecimalFormat(byte[] resultBytes)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in resultBytes)
            {
                stringBuilder.AppendFormat("{0:x2}", item);
            }
            return stringBuilder.ToString();
        }
    }
}
