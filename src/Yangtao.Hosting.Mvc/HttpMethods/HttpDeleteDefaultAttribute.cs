using Microsoft.AspNetCore.Mvc;

namespace Yangtao.Hosting.Mvc.HttpMethods
{
    public class HttpDeleteDefaultAttribute : HttpDeleteAttribute
    {
        public HttpDeleteDefaultAttribute() : base("/api/v{v:ApiVersion}/[controller]/") { }

        public HttpDeleteDefaultAttribute(string template) : base("/api/v{v:ApiVersion}/[controller]/" + template)
        {
        }
    }
}
