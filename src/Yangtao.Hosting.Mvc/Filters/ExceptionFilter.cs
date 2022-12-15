using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mime;
using Yangtao.Hosting.Core.HttpErrorResult;

namespace Yangtao.Hosting.Mvc.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var errorResult = GetErrorResponse(context.Exception);
            context.Result = new ActionErrorResult(errorResult);
        }

        private static HttpErrorResult GetErrorResponse(Exception exception)
        {
            if (exception is HttpErrorResult error) return error;

            return new HttpErrorResult(500, -1, exception.Message);
        }
    }

    internal class ActionErrorResult : IActionResult
    {
        private readonly HttpErrorResult HttpErrorResult;

        public ActionErrorResult(HttpErrorResult errorResult)
        {
            HttpErrorResult = errorResult;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = HttpErrorResult.HttpStatusCode;
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;

            var result = HttpErrorResult.GetErrorResultContent();
            await context.HttpContext.Response.WriteAsync(result);
        }
    }
}
