using Microsoft.AspNetCore.Mvc;

namespace Yangtao.Hosting.Mvc.HttpMethods
{
    internal class HttpPostDefaultAttribute : HttpPostAttribute
    {
        public HttpPostDefaultAttribute() : base("/api/v{v:ApiVersion}/[controller]")
        {
        }
    }
}
