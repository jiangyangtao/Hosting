using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult
{
    public class HttpForbidResult : HttpFailResult
    {
        public HttpForbidResult() : base(403) { }

        public HttpForbidResult(string message) : base(403, message) { }

        public HttpForbidResult(int code, string message) : base(403, code, message) { }
    }
}
