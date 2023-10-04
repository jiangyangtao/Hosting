using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.HmacShaPorviders
{
    internal class HmacSha512Provider : HmacShaProviderBase, IHmacShaProvider
    {
        public HmacSha512Provider(ISignatureValidationConfigurationProvider configurationProvider):base(configurationProvider)
        {
        }

        public HmacShaAlgorithmType HmacShaAlgorithmType => HmacShaAlgorithmType.HmacSha512;

        public string SignData(byte[] valueBytes)
        {
            throw new NotImplementedException();
        }

        public bool VerifyData(byte[] valueBytes, string signature)
        {
            throw new NotImplementedException();
        }
    }
}
