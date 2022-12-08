using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Mvc.HttpResponseFormat
{
    public class HttpResponseJsonResult : IActionResult
    {
        private int HttpStatusCode { set; get; } = 200;

        public static ResponseResult ResponseSuccess()
        {
            return new ResponseResult
            {
                Code = 0,
                Data = null,
                Message = string.Empty,
                Count = 0,
            };
        }

        public static ResponseResult ResponseSuccess<TData>(TData data, long count = 0)
        {
            return new ResponseResult
            {
                Code = 0,
                Data = data,
                Message = string.Empty,
                Count = count,
            };
        }

        public static ResponseResult ResponseSuccess<TData>(IEnumerable<TData> data, long count = 0)
        {
            return new ResponseResult
            {
                Code = 0,
                Data = data,
                Message = string.Empty,
                Count = count == 0 ? data.Count() : count,
            };
        }

        public static ResponseResult ResponseFail(string message = "")
        {
            return new ResponseResult
            {
                Code = 0,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public static ResponseResult ResponseFail(int code, string message = "")
        {
            return new ResponseResult
            {
                Code = code,
                Data = null,
                Message = message,
                Count = 0,
            };
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = HttpStatusCode;
            context.HttpContext.Response.ContentType = "application/json";

            var result = Error.GetResponseResult();
            await context.HttpContext.Response.WriteAsync(result);
        }
    }
}
