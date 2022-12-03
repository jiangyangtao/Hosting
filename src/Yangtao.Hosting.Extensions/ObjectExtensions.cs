using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Extensions
{
    public static class ObjectExtensions
    {
        public static string Serialize(this object value)
        {
            if (value == null) return string.Empty;

            return JsonConvert.SerializeObject(value);
        }
    }
}
