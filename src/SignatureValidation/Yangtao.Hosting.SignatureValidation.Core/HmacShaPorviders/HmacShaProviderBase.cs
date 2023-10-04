using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Core.HmacShaPorviders
{
    internal abstract class HmacShaProviderBase
    {
        private readonly HmacShaSignatureFormatType _signatureFormatType;

        protected HmacShaProviderBase(HmacShaSignatureFormatType signatureFormatType)
        {
            _signatureFormatType = signatureFormatType;
        }


    }
}
