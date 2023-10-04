using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.SignatureValidation.Core.Abstractions
{
    internal interface IHmacShaPorviderFactory
    {
        IHmacShaProvider CreateIHmacShaProvider();
    }
}
