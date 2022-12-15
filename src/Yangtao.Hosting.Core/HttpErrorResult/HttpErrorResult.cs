
using Newtonsoft.Json;

namespace Yangtao.Hosting.Core.HttpErrorResult
{
    public class HttpErrorResult : Exception
    {
        public HttpErrorResult(int httpStatusCode, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            ErrorResult = ErrorResult.CreateErrorResult(message);
        }

        public HttpErrorResult(int httpStatusCode, int code, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            ErrorResult = ErrorResult.CreateErrorResult(code, message);
        }

        public int HttpStatusCode { get; private set; }

        private ErrorResult ErrorResult { get; set; }

        public ErrorResult GetErrorResult() => ErrorResult;

        public string GetErrorResultContent() => JsonConvert.SerializeObject(ErrorResult);
    }
}
