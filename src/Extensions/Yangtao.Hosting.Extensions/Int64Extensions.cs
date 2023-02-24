using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Extensions
{
    public static class Int64Extensions
    {
        public static long Value(this long? value, long defaultValue = 0)
        {
            if (value.HasValue == false) return defaultValue;

            return value.Value;
        }
    }
}
