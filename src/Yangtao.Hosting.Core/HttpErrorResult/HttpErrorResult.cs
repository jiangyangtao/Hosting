
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




        public static void ResponseBadRequest(string message) => throw new HttpErrorResult(400, message);

        public static void ResponseBadRequest(int code, string message) => throw new HttpErrorResult(400, code, message);



        public static void ResponseConflict(string message) => throw new HttpErrorResult(409, message);

        public static void ResponseConflict(int code, string message) => throw new HttpErrorResult(409, code, message);



        public static void ResponseForbid(string message) => throw new HttpErrorResult(403, message);

        public static void ResponseForbid(int code, string message) => throw new HttpErrorResult(403, code, message);



        public static void ResponseNotFound(string message) => throw new HttpErrorResult(404, message);

        public static void ResponseNotFound(int code, string message) => throw new HttpErrorResult(404, code, message);



        public static void ResponseUnauthorized(string message) => throw new HttpErrorResult(401, message);

        public static void ResponseUnauthorized(int code, string message) => throw new HttpErrorResult(401, code, message);



        public static void ResponseServerError(string message) => throw new HttpErrorResult(500, message);

        public static void ResponseServerError(int code, string message) => throw new HttpErrorResult(500, code, message);
    }
}
