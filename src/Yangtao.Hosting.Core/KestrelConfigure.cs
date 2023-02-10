using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Core
{
    public class KestrelConfigure
    {
        public Endpoint[] Endpoints { set; get; }
    }

    public class Endpoint
    {
        public int Port { set; get; }

        public Certificate Certificate { set; get; }
    }

    public class Certificate
    {
        public string Path { set; get; }

        public string FileName { set; get; }

        public string Password { set; get; }
    }
}
