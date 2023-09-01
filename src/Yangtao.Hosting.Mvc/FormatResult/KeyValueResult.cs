using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Mvc.FormatResult
{
    public class KeyValueResult
    {
        public KeyValueResult(string value, string label)
        {
            this.value = value;
            this.label = label;
        }

        public string value { set; get; }

        public string label { set; get; }
    }
}
