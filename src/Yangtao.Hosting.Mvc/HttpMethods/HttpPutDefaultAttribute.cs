using Microsoft.AspNetCore.Mvc;

namespace Yangtao.Hosting.Mvc.HttpMethods
{
    internal class HttpPutDefaultAttribute : HttpPutAttribute
    {
        public HttpPutDefaultAttribute() : base("/api/v{v:ApiVersion}/[controller]")
        {
        }
    }
}
