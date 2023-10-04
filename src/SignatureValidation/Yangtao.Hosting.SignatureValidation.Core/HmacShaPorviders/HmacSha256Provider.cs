using System.Security.Cryptography;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.HmacShaPorviders
{
    internal class HmacSha256Provider : HmacShaProviderBase, IHmacShaProvider
    {
        private readonly HMACSHA256 hmacSha256;

        public HmacSha256Provider(ISignatureValidationConfigurationProvider configurationProvider) : base(configurationProvider)
        {
            if (configurationProvider.IsHmacShaSignature == false) return;
            if (_hmacShaConfiguration.HmacShaAlgorithmType != HmacShaAlgorithmType) return;

            hmacSha256 = new HMACSHA256();
        }

        public HmacShaAlgorithmType HmacShaAlgorithmType => HmacShaAlgorithmType.HmacSha256;

        public string SignData(byte[] valueBytes)
        {
            var hash = hmacSha256.ComputeHash(valueBytes);
            return FormatSignature(hash);
        }

        public bool VerifyData(byte[] valueBytes, string signature)
        {
            var computeSignValue = SignData(valueBytes);
            return computeSignValue == signature;
        }
    }
}
