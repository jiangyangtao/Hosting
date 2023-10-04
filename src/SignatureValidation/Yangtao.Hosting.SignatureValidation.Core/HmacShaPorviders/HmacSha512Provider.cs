using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.HmacShaPorviders
{
    internal class HmacSha512Provider : HmacShaProviderBase, IHmacShaProvider
    {
        public HmacSha512Provider(HmacShaSignatureFormatType signatureFormatType) : base(signatureFormatType)
        {
        }
    }
}
