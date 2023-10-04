using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.HmacShaPorviders
{
    internal class HmacSha384Provider : HmacShaProviderBase, IHmacShaProvider
    {
        private readonly HMACSHA384 hmacSha384;

        public HmacSha384Provider(ISignatureValidationConfigurationProvider configurationProvider) : base(configurationProvider)
        {
        }

        public HmacShaAlgorithmType HmacShaAlgorithmType => HmacShaAlgorithmType.HmacSha384;

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
