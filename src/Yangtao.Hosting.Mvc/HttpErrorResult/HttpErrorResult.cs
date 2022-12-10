
using Newtonsoft.Json;
using Yangtao.Hosting.Mvc.Abstractions;

namespace Yangtao.Hosting.Mvc.HttpErrorResult
{
    internal class HttpErrorResult : Exception
    {
        public HttpErrorResult(int httpStatusCode, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            ErrorResult = ErrorResult.CreateErrorResult(message);
        }

        internal HttpErrorResult(int httpStatusCode, int code, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            ErrorResult = ErrorResult.CreateErrorResult(code, message);
        }

        public int HttpStatusCode { get; private set; }

        private ErrorResult ErrorResult { get; set; }

        public string GetErrorResultContent() => JsonConvert.SerializeObject(ErrorResult);
    }
}
