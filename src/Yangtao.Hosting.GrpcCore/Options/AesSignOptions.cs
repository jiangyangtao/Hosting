using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.GrpcCore.Options
{
    public class AesSignOptions
    {
        public string Iv { set; get; }

        public string SecurityKey { set; get; }
    }
}
