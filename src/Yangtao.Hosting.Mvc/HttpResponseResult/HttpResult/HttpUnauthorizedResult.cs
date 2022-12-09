using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult
{
    public class HttpUnauthorizedResult : HttpFailResult
    {
        public HttpUnauthorizedResult(string message) : base(401, message) { }

        public HttpUnauthorizedResult(int code, string message) : base(401, code, message) { }
    }
}
