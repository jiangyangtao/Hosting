using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Enum
{
    internal class EnumProvider<TEnum> : EnumHandlerBase, IEnumProvider<TEnum> where TEnum : struct, System.Enum
    {
        public EnumProvider() : base(typeof(TEnum))
        {
        }
    }
}
