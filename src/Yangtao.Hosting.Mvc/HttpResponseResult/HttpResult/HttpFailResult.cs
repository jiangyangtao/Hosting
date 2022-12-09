using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult
{
    public class HttpFailResult : HttpResponseResult
    {
        public HttpFailResult(int httpStatusCode, string message) : base(httpStatusCode, ResponseResult.CreateFailResult(message)) { }

        public HttpFailResult(int httpStatusCode, int code, string message) : base(httpStatusCode, ResponseResult.CreateFailResult(code, message)) { }
    }
}
