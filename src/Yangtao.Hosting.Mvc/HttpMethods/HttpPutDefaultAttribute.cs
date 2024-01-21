using Microsoft.AspNetCore.Mvc;

namespace Yangtao.Hosting.Mvc.HttpMethods
{
    public class HttpPutDefaultAttribute : HttpPutAttribute
    {
        public HttpPutDefaultAttribute() : base("/api/v{v:ApiVersion}/[controller]/") { }

        public HttpPutDefaultAttribute(string template) : base("/api/v{v:ApiVersion}/[controller]/" + template)
        {
        }
    }
}
