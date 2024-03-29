﻿namespace Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult
{
    public class HttpBadRequestResult : HttpFailResult
    {
        public HttpBadRequestResult() : base(400) { }

        public HttpBadRequestResult(string message) : base(400, message) { }

        public HttpBadRequestResult(int code, string message) : base(400, code, message) { }
    }
}
