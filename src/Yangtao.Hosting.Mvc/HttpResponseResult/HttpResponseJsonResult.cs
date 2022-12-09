using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Yangtao.Hosting.Mvc.HttpResponseResult
{
    public class HttpResponseJsonResult : IActionResult
    {
        public HttpResponseJsonResult(int httpStatusCode, ResponseResult responseResult)
        {
            HttpStatusCode = httpStatusCode;
            ResponseResult = responseResult;
        }

        private int HttpStatusCode { set; get; } = 200;

        private ResponseResult ResponseResult { set; get; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = HttpStatusCode;
            context.HttpContext.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(ResponseResult);
            await context.HttpContext.Response.WriteAsync(result);
        }
    }
}
