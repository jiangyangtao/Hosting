using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult
{
    public class HttpConflictResult : HttpFailResult
    {
        public HttpConflictResult() : base(409) { }

        public HttpConflictResult(string message) : base(409, message) { }

        public HttpConflictResult(int code, string message) : base(409, code, message) { }
    }
}
