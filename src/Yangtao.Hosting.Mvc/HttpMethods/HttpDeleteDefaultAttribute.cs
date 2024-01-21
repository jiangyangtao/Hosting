using Microsoft.AspNetCore.Mvc;

namespace Yangtao.Hosting.Mvc.HttpMethods
{
    internal class HttpDeleteDefaultAttribute : HttpDeleteAttribute
    {
        public HttpDeleteDefaultAttribute() : base("/api/v{v:ApiVersion}/[controller]")
        {
        }
    }
}
