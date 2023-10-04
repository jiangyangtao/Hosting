using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.HmacShaPorviders
{
    internal class HmacSha256Provider : HmacShaProviderBase, IHmacShaProvider
    {
        public HmacSha256Provider(HmacShaSignatureFormatType signatureFormatType) : base(signatureFormatType)
        {
        }

        public string SignData(string value)
        {
            throw new NotImplementedException();
        }

        public bool VerifyData(string value, string signature)
        {
            throw new NotImplementedException();
        }
    }
}
