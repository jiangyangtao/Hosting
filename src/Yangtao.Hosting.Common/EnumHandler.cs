using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Common
{
    public class EnumHandler<TEnum> : IDisposable where TEnum : struct, Enum
    {
        private readonly Type EnumType;

        private EnumHandler()
        {
            EnumType = typeof(TEnum);
        }

        public static EnumHandler<TEnum> Create() => new();



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
