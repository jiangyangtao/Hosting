
namespace Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult
{
    public class HttpNotFoundResult : HttpFailResult
    {
        public HttpNotFoundResult() : base(404) { }

        public HttpNotFoundResult(string message) : base(404, message) { }

        public HttpNotFoundResult(int code, string message) : base(404, code, message) { }
    }
}
