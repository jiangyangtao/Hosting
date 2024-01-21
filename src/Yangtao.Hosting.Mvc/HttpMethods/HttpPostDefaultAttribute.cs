using Microsoft.AspNetCore.Mvc;

namespace Yangtao.Hosting.Mvc.HttpMethods
{
    public class HttpPostDefaultAttribute : HttpPostAttribute
    {
        public HttpPostDefaultAttribute() : base("/api/v{v:ApiVersion}/[controller]/") { }

        public HttpPostDefaultAttribute(string template) : base("/api/v{v:ApiVersion}/[controller]/" + template)
        {
        }
    }
}
