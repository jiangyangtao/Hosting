using System.Security.Cryptography;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    internal class HmacShaProvider : IHmacShaProvider
    {
        private readonly HmacShaConfiguration _hmacShaConfiguration;

        public HmacShaProvider(ISignatureValidationConfigurationProvider configurationProvider)
        {
            if (configurationProvider.IsHmacShaSignature)
            {
                _hmacShaConfiguration = configurationProvider.GetHmacShaConfiguration();
            }
        }

        public string SignData(string value)
        {
            //HMACSHA256
            //HMACSHA384
            throw new NotImplementedException();
        }

        public bool VerifyData(string value, string signature)
        {
            throw new NotImplementedException();
        }
    }
}
