using System.Security.Cryptography;
using System.Text;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;
using Yangtao.Hosting.Extensions;
using Microsoft.Extensions.Options;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    internal class HmacShaProvider : IHmacShaProvider
    {
        private readonly HmacShaConfiguration _hmacShaConfiguration;

        public HmacShaProvider(
            IOptions<SignatureValidationConfiguration> signatureValidationConfigurationOptions,
            IOptions<HmacShaConfiguration> hmacShaConfigurationOptions)
        {
            if (signatureValidationConfigurationOptions.Value.IsHmacShaSignature && hmacShaConfigurationOptions.Value == null)
                throw new NullReferenceException(nameof(HmacShaConfiguration));

            _hmacShaConfiguration = hmacShaConfigurationOptions.Value;
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
            if (_hmacShaConfiguration.HmacShaAlgorithmType == HmacShaAlgorithmType.HmacSha256)
                return new HMACSHA256(_hmacShaConfiguration.SecretKeyBytes);

            if (_hmacShaConfiguration.HmacShaAlgorithmType == HmacShaAlgorithmType.HmacSha384)
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
