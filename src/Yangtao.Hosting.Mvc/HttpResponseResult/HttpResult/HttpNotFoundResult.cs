using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult
{
    public class HttpNotFoundResult : HttpFailResult
    {
        public HttpNotFoundResult(string message) : base(404, message) { }

        public HttpNotFoundResult(int code, string message) : base(404, code, message) { }
    }
}
