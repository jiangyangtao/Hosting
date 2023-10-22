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



        [NonAction]
        public Task<HttpBadRequestResult> ResponseBadRequestAsync() => Task.FromResult(new HttpBadRequestResult());

        [NonAction]
        public Task<HttpBadRequestResult> ResponseBadRequestAsync(string message) => Task.FromResult(new HttpBadRequestResult(message));

        [NonAction]
        public Task<HttpBadRequestResult> ResponseBadRequestAsync(int code, string message) => Task.FromResult(new HttpBadRequestResult(code, message));

        [NonAction]
        public Task<TResult> ResponseBadRequestAsync<TResult>() => HttpErrorResult.ResponseBadRequestAsync<TResult>();

        [NonAction]
        public Task<TResult> ResponseBadRequestAsync<TResult>(string message) => HttpErrorResult.ResponseBadRequestAsync<TResult>(message);

        [NonAction]
        public Task<TResult> ResponseBadRequestAsync<TResult>(int code, string message) => HttpErrorResult.ResponseBadRequestAsync<TResult>(code, message);


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



        [NonAction]
        public Task<HttpConflictResult> ResponseConflictAsync() => Task.FromResult(new HttpConflictResult());

        [NonAction]
        public Task<HttpConflictResult> ResponseConflictAsync(string message) => Task.FromResult(new HttpConflictResult(message));

        [NonAction]
        public Task<HttpConflictResult> ResponseConflictAsync(int code, string message) => Task.FromResult(new HttpConflictResult(code, message));

        [NonAction]
        public Task<TResult> ResponseConflictAsync<TResult>() => HttpErrorResult.ResponseConflictAsync<TResult>();

        [NonAction]
        public Task<TResult> ResponseConflictAsync<TResult>(string message) => HttpErrorResult.ResponseConflictAsync<TResult>(message);

        [NonAction]
        public Task<TResult> ResponseConflictAsync<TResult>(int code, string message) => HttpErrorResult.ResponseConflictAsync<TResult>(code, message);

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


        [NonAction]
        public Task<HttpForbidResult> ResponseForbidAsync() => Task.FromResult(new HttpForbidResult());

        [NonAction]
        public Task<HttpForbidResult> ResponseForbidAsync(string message) => Task.FromResult(new HttpForbidResult(message));

        [NonAction]
        public Task<HttpForbidResult> ResponseForbidAsync(int code, string message) => Task.FromResult(new HttpForbidResult(code, message));

        [NonAction]
        public Task<TResult> ResponseForbidAsync<TResult>() => HttpErrorResult.ResponseForbidAsync<TResult>();

        [NonAction]
        public Task<TResult> ResponseForbidAsync<TResult>(string message) => HttpErrorResult.ResponseForbidAsync<TResult>(message);

        [NonAction]
        public Task<TResult> ResponseForbidAsync<TResult>(int code, string message) => HttpErrorResult.ResponseForbidAsync<TResult>(code, message);

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



        [NonAction]
        public Task<HttpNotFoundResult> ResponseNotFoundAsync() => Task.FromResult(new HttpNotFoundResult());

        [NonAction]
        public Task<HttpNotFoundResult> ResponseNotFoundAsync(string message) => Task.FromResult(new HttpNotFoundResult(message));

        [NonAction]
        public Task<HttpNotFoundResult> ResponseNotFoundAsync(int code, string message) => Task.FromResult(new HttpNotFoundResult(code, message));

        [NonAction]
        public Task<TResult> ResponseNotFoundAsync<TResult>() => HttpErrorResult.ResponseNotFoundAsync<TResult>();

        [NonAction]
        public Task<TResult> ResponseNotFoundAsync<TResult>(string message) => HttpErrorResult.ResponseNotFoundAsync<TResult>(message);

        [NonAction]
        public Task<TResult> ResponseNotFoundAsync<TResult>(int code, string message) => HttpErrorResult.ResponseNotFoundAsync<TResult>(code, message);

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



        [NonAction]
        public Task<HttpServerErrorResult> ResponseServerErrorAsync(string message) => Task.FromResult(new HttpServerErrorResult(message));

        [NonAction]
        public Task<HttpServerErrorResult> ResponseServerErrorAsync(int code, string message) => Task.FromResult(new HttpServerErrorResult(code, message));

        [NonAction]
        public Task<TResult> ResponseServerErrorAsync<TResult>(string message) => HttpErrorResult.ResponseServerErrorAsync<TResult>(message);

        [NonAction]
        public Task<TResult> ResponseServerErrorAsync<TResult>(int code, string message) => HttpErrorResult.ResponseServerErrorAsync<TResult>(code, message);

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




        [NonAction]
        public Task<HttpUnauthorizedResult> ResponseUnauthorizedAsync() => Task.FromResult(new HttpUnauthorizedResult());

        [NonAction]
        public Task<HttpUnauthorizedResult> ResponseUnauthorizedAsync(string message) => Task.FromResult(new HttpUnauthorizedResult(message));

        [NonAction]
        public Task<HttpUnauthorizedResult> ResponseUnauthorizedAsync(int code, string message) => Task.FromResult(new HttpUnauthorizedResult(code, message));

        [NonAction]
        public Task<TResult> ResponseUnauthorizedAsync<TResult>() => HttpErrorResult.ResponseUnauthorizedAsync<TResult>();

        [NonAction]
        public Task<TResult> ResponseUnauthorizedAsync<TResult>(string message) => HttpErrorResult.ResponseUnauthorizedAsync<TResult>(message);

        [NonAction]
        public Task<TResult> ResponseUnauthorizedAsync<TResult>(int code, string message) => HttpErrorResult.ResponseUnauthorizedAsync<TResult>(code, message);

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
