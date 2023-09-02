using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Yangtao.Hosting.Abstractions.IResult;



namespace Yangtao.Hosting.Mvc.HttpResponseResult
{
    public class HttpResponseResult : ObjectResult
    {
        public HttpResponseResult(int httpStatusCode) : base(null) => StatusCode = httpStatusCode;

        public HttpResponseResult(int httpStatusCode, IResponseResult responseResult) : base(responseResult)
        {
            StatusCode = httpStatusCode;
            ContentTypes.Add(MediaTypeNames.Application.Json);
        }
    }
}
