
namespace Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult
{
    public class HttpUnauthorizedResult : HttpFailResult
    {
        public HttpUnauthorizedResult() : base(401) { }

        public HttpUnauthorizedResult(string message) : base(401, message) { }

        public HttpUnauthorizedResult(int code, string message) : base(401, code, message) { }
    }
}
