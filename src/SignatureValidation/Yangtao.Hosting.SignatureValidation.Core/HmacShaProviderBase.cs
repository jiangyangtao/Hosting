using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    public abstract class HmacShaProviderBase
    {
        private readonly HmacShaOptions _hmacShaConfiguration;

        public HmacShaProviderBase(bool isHmacShaSignature, HmacShaOptions? hmacShaConfigurationOptions)
        {
            if (isHmacShaSignature) return;

            _hmacShaConfiguration = hmacShaConfigurationOptions ?? throw new NullReferenceException(nameof(HmacShaOptions));
        }

        public string SignData(string value)
        {
            if (value.IsNullOrEmpty()) throw new ArgumentNullException(nameof(value));

            var valueBytes = Encoding.UTF8.GetBytes(value);
            using var hashAlgorithm = GetHashAlgorithm();
            var resultBytes = hashAlgorithm.ComputeHash(valueBytes);

            return FormatSignature(resultBytes);
        }

        public bool VerifyData(string value, string signature)
        {
            if (signature.IsNullOrEmpty()) throw new ArgumentNullException(nameof(signature));

            var computeSignValue = SignData(value);
            return computeSignValue == signature;
        }

        private HashAlgorithm GetHashAlgorithm()
        {
            if (_hmacShaConfiguration.HmacShaAlgorithmType == HashAlgorithmType.HmacSha256)
                return new HMACSHA256(_hmacShaConfiguration.SecretKeyBytes);

            if (_hmacShaConfiguration.HmacShaAlgorithmType == HashAlgorithmType.HmacSha384)
                return new HMACSHA384(_hmacShaConfiguration.SecretKeyBytes);

            return new HMACSHA512(_hmacShaConfiguration.SecretKeyBytes);
        }

        private string FormatSignature(byte[] resultBytes)
        {
            if (_hmacShaConfiguration.HmacShaSignatureFormatType == HmacShaSignatureFormatType.Base64)
                return Convert.ToBase64String(resultBytes);

            return ToHexadecimalFormat(resultBytes);
        }

        private static string ToHexadecimalFormat(byte[] resultBytes)
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
