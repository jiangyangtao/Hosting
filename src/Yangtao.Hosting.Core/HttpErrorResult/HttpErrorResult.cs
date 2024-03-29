﻿using Newtonsoft.Json;

namespace Yangtao.Hosting.Core.HttpErrorResult
{
    public class HttpErrorResult : Exception
    {
        public HttpErrorResult(int httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
            ErrorResult = null;
        }

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

        public string GetErrorResultContent()
        {
            if (ErrorResult == null) return string.Empty;

            return JsonConvert.SerializeObject(ErrorResult);
        }

        #region BadRequest


        public static void ResponseBadRequest() => throw new HttpErrorResult(400);

        public static void ResponseBadRequest(string message) => throw new HttpErrorResult(400, message);

        public static void ResponseBadRequest(int code, string message) => throw new HttpErrorResult(400, code, message);

        public static TResult ResponseBadRequest<TResult>() => throw new HttpErrorResult(400);

        public static TResult ResponseBadRequest<TResult>(string message) => throw new HttpErrorResult(400, message);

        public static TResult ResponseBadRequest<TResult>(int code, string message) => throw new HttpErrorResult(400, code, message);

        public static Task ResponseBadRequestAsync() => throw new HttpErrorResult(400);

        public static Task ResponseBadRequestAsync(string message) => throw new HttpErrorResult(400, message);

        public static Task ResponseBadRequestAsync(int code, string message) => throw new HttpErrorResult(400, code, message);

        public static Task<TResult> ResponseBadRequestAsync<TResult>() => throw new HttpErrorResult(400);

        public static Task<TResult> ResponseBadRequestAsync<TResult>(string message) => throw new HttpErrorResult(400, message);

        public static Task<TResult> ResponseBadRequestAsync<TResult>(int code, string message) => throw new HttpErrorResult(400, code, message);


        #endregion

        #region Conflict     

        public static void ResponseConflict() => throw new HttpErrorResult(409);

        public static void ResponseConflict(string message) => throw new HttpErrorResult(409, message);

        public static void ResponseConflict(int code, string message) => throw new HttpErrorResult(409, code, message);

        public static TResult ResponseConflict<TResult>() => throw new HttpErrorResult(409);

        public static TResult ResponseConflict<TResult>(string message) => throw new HttpErrorResult(409, message);

        public static TResult ResponseConflict<TResult>(int code, string message) => throw new HttpErrorResult(409, code, message);

        public static Task ResponseConflictAsync() => throw new HttpErrorResult(409);

        public static Task ResponseConflictAsync(string message) => throw new HttpErrorResult(409, message);

        public static Task ResponseConflictAsync(int code, string message) => throw new HttpErrorResult(409, code, message);

        public static Task<TResult> ResponseConflictAsync<TResult>() => throw new HttpErrorResult(409);

        public static Task<TResult> ResponseConflictAsync<TResult>(string message) => throw new HttpErrorResult(409, message);

        public static Task<TResult> ResponseConflictAsync<TResult>(int code, string message) => throw new HttpErrorResult(409, code, message);

        #endregion

        #region Forbid


        public static void ResponseForbid() => throw new HttpErrorResult(403);

        public static void ResponseForbid(string message) => throw new HttpErrorResult(403, message);

        public static void ResponseForbid(int code, string message) => throw new HttpErrorResult(403, code, message);

        public static TResult ResponseForbid<TResult>() => throw new HttpErrorResult(403);

        public static TResult ResponseForbid<TResult>(string message) => throw new HttpErrorResult(403, message);

        public static TResult ResponseForbid<TResult>(int code, string message) => throw new HttpErrorResult(403, code, message);


        public static Task ResponseForbidAsync() => throw new HttpErrorResult(403);

        public static Task ResponseForbidAsync(string message) => throw new HttpErrorResult(403, message);

        public static Task ResponseForbidAsync(int code, string message) => throw new HttpErrorResult(403, code, message);

        public static Task<TResult> ResponseForbidAsync<TResult>() => throw new HttpErrorResult(403);

        public static Task<TResult> ResponseForbidAsync<TResult>(string message) => throw new HttpErrorResult(403, message);

        public static Task<TResult> ResponseForbidAsync<TResult>(int code, string message) => throw new HttpErrorResult(403, code, message);

        #endregion

        #region NotFound

        public static void ResponseNotFound() => throw new HttpErrorResult(404);

        public static void ResponseNotFound(string message) => throw new HttpErrorResult(404, message);

        public static void ResponseNotFound(int code, string message) => throw new HttpErrorResult(404, code, message);

        public static TResult ResponseNotFound<TResult>() => throw new HttpErrorResult(404);

        public static TResult ResponseNotFound<TResult>(string message) => throw new HttpErrorResult(404, message);

        public static TResult ResponseNotFound<TResult>(int code, string message) => throw new HttpErrorResult(404, code, message);


        public static Task ResponseNotFoundAsync() => throw new HttpErrorResult(404);

        public static Task ResponseNotFoundAsync(string message) => throw new HttpErrorResult(404, message);

        public static Task ResponseNotFoundAsync(int code, string message) => throw new HttpErrorResult(404, code, message);

        public static Task<TResult> ResponseNotFoundAsync<TResult>() => throw new HttpErrorResult(404);

        public static Task<TResult> ResponseNotFoundAsync<TResult>(string message) => throw new HttpErrorResult(404, message);

        public static Task<TResult> ResponseNotFoundAsync<TResult>(int code, string message) => throw new HttpErrorResult(404, code, message);

        #endregion

        #region Unauthorized

        public static void ResponseUnauthorized() => throw new HttpErrorResult(401);

        public static void ResponseUnauthorized(string message) => throw new HttpErrorResult(401, message);

        public static void ResponseUnauthorized(int code, string message) => throw new HttpErrorResult(401, code, message);

        public static TResult ResponseUnauthorized<TResult>() => throw new HttpErrorResult(401);

        public static TResult ResponseUnauthorized<TResult>(string message) => throw new HttpErrorResult(401, message);

        public static TResult ResponseUnauthorized<TResult>(int code, string message) => throw new HttpErrorResult(401, code, message);


        public static void ResponseUnauthorizedAsync() => throw new HttpErrorResult(401);

        public static void ResponseUnauthorizedAsync(string message) => throw new HttpErrorResult(401, message);

        public static void ResponseUnauthorizedAsync(int code, string message) => throw new HttpErrorResult(401, code, message);

        public static Task<TResult> ResponseUnauthorizedAsync<TResult>() => throw new HttpErrorResult(401);

        public static Task<TResult> ResponseUnauthorizedAsync<TResult>(string message) => throw new HttpErrorResult(401, message);

        public static Task<TResult> ResponseUnauthorizedAsync<TResult>(int code, string message) => throw new HttpErrorResult(401, code, message);

        #endregion

        #region ServerError   

        public static void ResponseServerError() => throw new HttpErrorResult(500);

        public static void ResponseServerError(string message) => throw new HttpErrorResult(500, message);

        public static void ResponseServerError(int code, string message) => throw new HttpErrorResult(500, code, message);

        public static TResult ResponseServerError<TResult>() => throw new HttpErrorResult(500);

        public static TResult ResponseServerError<TResult>(string message) => throw new HttpErrorResult(500, message);

        public static TResult ResponseServerError<TResult>(int code, string message) => throw new HttpErrorResult(500, code, message);


        public static Task ResponseServerErrorAsync() => throw new HttpErrorResult(500);

        public static Task ResponseServerErrorAsync(string message) => throw new HttpErrorResult(500, message);

        public static Task ResponseServerErrorAsync(int code, string message) => throw new HttpErrorResult(500, code, message);

        public static Task<TResult> ResponseServerErrorAsync<TResult>() => throw new HttpErrorResult(500);

        public static Task<TResult> ResponseServerErrorAsync<TResult>(string message) => throw new HttpErrorResult(500, message);

        public static Task<TResult> ResponseServerErrorAsync<TResult>(int code, string message) => throw new HttpErrorResult(500, code, message);

        #endregion
    }
}
