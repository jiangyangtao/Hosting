using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Extensions
{
    public static class Int32Extensions
    {
        public static int Value(this int? value, int defaultValue = 0)
        {
            if (value.HasValue == false) return defaultValue;

            return value.Value;
        }
    }
}
