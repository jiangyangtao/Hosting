using Microsoft.AspNetCore.Mvc;
using Yangtao.Hosting.Core.HttpErrorResult;
using Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult;

namespace Yangtao.Hosting.Controller
{
    public abstract class HttpResultController : ControllerBase
    {
        #region BadRequest  

        [NonAction]
        public HttpBadRequestResult ResponseBadRequest() => new();

        [NonAction]
        public HttpBadRequestResult ResponseBadRequest(string message) => new(message);

        [NonAction]
        public HttpBadRequestResult ResponseBadRequest(int code, string message) => new(code, message);

        [NonAction]
        public TResult ResponseBadRequest<TResult>() => HttpErrorResult.ResponseBadRequest<TResult>();

        [NonAction]
        public TResult ResponseBadRequest<TResult>(string message) => HttpErrorResult.ResponseBadRequest<TResult>(message);

        [NonAction]
        public TResult ResponseBadRequest<TResult>(int code, string message) => HttpErrorResult.ResponseBadRequest<TResult>(code, message);

        #endregion

        #region Conflict

        [NonAction]
        public HttpConflictResult ResponseConflict() => new();

        [NonAction]
        public HttpConflictResult ResponseConflict(string message) => new(message);

        [NonAction]
        public HttpConflictResult ResponseConflict(int code, string message) => new(code, message);

        [NonAction]
        public TResult ResponseConflict<TResult>() => HttpErrorResult.ResponseConflict<TResult>();

        [NonAction]
        public TResult ResponseConflict<TResult>(string message) => HttpErrorResult.ResponseConflict<TResult>(message);

        [NonAction]
        public TResult ResponseConflict<TResult>(int code, string message) => HttpErrorResult.ResponseConflict<TResult>(code, message);

        #endregion

        #region Forbid      

        [NonAction]
        public HttpForbidResult ResponseForbid() => new();

        [NonAction]
        public HttpForbidResult ResponseForbid(string message) => new(message);

        [NonAction]
        public HttpForbidResult ResponseForbid(int code, string message) => new(code, message);

        [NonAction]
        public TResult ResponseForbid<TResult>() => HttpErrorResult.ResponseForbid<TResult>();

        [NonAction]
        public TResult ResponseForbid<TResult>(string message) => HttpErrorResult.ResponseForbid<TResult>(message);

        [NonAction]
        public TResult ResponseForbid<TResult>(int code, string message) => HttpErrorResult.ResponseForbid<TResult>(code, message);

        #endregion

        #region NotFound


        [NonAction]
        public HttpNotFoundResult ResponseNotFound() => new();

        [NonAction]
        public HttpNotFoundResult ResponseNotFound(string message) => new(message);

        [NonAction]
        public HttpNotFoundResult ResponseNotFound(int code, string message) => new(code, message);

        [NonAction]
        public TResult ResponseNotFound<TResult>() => HttpErrorResult.ResponseNotFound<TResult>();

        [NonAction]
        public TResult ResponseNotFound<TResult>(string message) => HttpErrorResult.ResponseNotFound<TResult>(message);

        [NonAction]
        public TResult ResponseNotFound<TResult>(int code, string message) => HttpErrorResult.ResponseNotFound<TResult>(code, message);

        #endregion

        #region ServerError    

        [NonAction]
        public HttpServerErrorResult ResponseServerError(string message) => new(message);

        [NonAction]
        public HttpServerErrorResult ResponseServerError(int code, string message) => new(code, message);

        [NonAction]
        public TResult ResponseServerError<TResult>(string message) => HttpErrorResult.ResponseServerError<TResult>(message);

        [NonAction]
        public TResult ResponseServerError<TResult>(int code, string message) => HttpErrorResult.ResponseServerError<TResult>(code, message);

        #endregion

        #region Unauthorized 

        [NonAction]
        public HttpUnauthorizedResult ResponseUnauthorized() => new();

        [NonAction]
        public HttpUnauthorizedResult ResponseUnauthorized(string message) => new(message);

        [NonAction]
        public HttpUnauthorizedResult ResponseUnauthorized(int code, string message) => new(code, message);

        [NonAction]
        public TResult ResponseUnauthorized<TResult>() => HttpErrorResult.ResponseUnauthorized<TResult>();

        [NonAction]
        public TResult ResponseUnauthorized<TResult>(string message) => HttpErrorResult.ResponseUnauthorized<TResult>(message);

        [NonAction]
        public TResult ResponseUnauthorized<TResult>(int code, string message) => HttpErrorResult.ResponseUnauthorized<TResult>(code, message);

        #endregion

        #region Success    

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

        #endregion

    }
}
