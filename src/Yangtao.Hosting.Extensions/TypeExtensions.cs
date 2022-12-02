using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papaya.Hosting.Extensions
{
    public static class TypeExtensions
    {
        public static string GetDisplayName(this Type type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return type.FullName;
        }
    }
}
