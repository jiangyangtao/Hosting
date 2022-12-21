using Microsoft.AspNetCore.Mvc;
using Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult;

namespace Yangtao.Hosting.Controller
{
    public abstract class HttpResultController : ControllerBase
    {
        [NonAction]
        public HttpBadRequestResult ResponseBadRequest(string message) => new(message);

        [NonAction]
        public HttpBadRequestResult ResponseBadRequest(int code, string message) => new(code, message);





        [NonAction]
        public HttpConflictResult ResponseConflict(string message) => new(message);

        [NonAction]
        public HttpConflictResult ResponseConflict(int code, string message) => new(code, message);





        [NonAction]
        public HttpForbidResult ResponseForbid(string message) => new(message);

        [NonAction]
        public HttpForbidResult ResponseForbid(int code, string message) => new(code, message);





        [NonAction]
        public HttpNotFoundResult ResponseNotFound(string message) => new(message);

        [NonAction]
        public HttpNotFoundResult ResponseNotFound(int code, string message) => new(code, message);





        [NonAction]
        public HttpServerErrorResult ResponseServerError(string message) => new(message);

        [NonAction]
        public HttpServerErrorResult ResponseServerError(int code, string message) => new(code, message);







        [NonAction]
        public HttpUnauthorizedResult ResponseUnauthorized(string message) => new(message);

        [NonAction]
        public HttpUnauthorizedResult ResponseUnauthorized(int code, string message) => new(code, message);






        [NonAction]
        public HttpSuccessResult ResponseSuccess() => new();

        [NonAction]
        public HttpSuccessResult<TResult> ResponseSuccess<TResult>(TResult result) => new(result);

        [NonAction]
        public HttpSuccessResult<TResult> ResponseSuccess<TResult>(TResult result, int count = 0) => new(result, count);

        [NonAction]
        public HttpSuccessResult<TResult> ResponseSuccess<TResult>(IEnumerable<TResult> results) => new(results);

        [NonAction]
        public HttpSuccessResult<TResult> ResponseSuccess<TResult>(IEnumerable<TResult> results, int count = 0) => new(results, count);

    }
}
