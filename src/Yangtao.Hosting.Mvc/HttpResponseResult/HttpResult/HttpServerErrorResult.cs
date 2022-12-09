using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult
{
    public class HttpServerErrorResult : HttpFailResult
    {
        public HttpServerErrorResult(string message) : base(500, message) { }

        public HttpServerErrorResult(int code, string message) : base(500, code, message) { }
    }
}
