using Microsoft.AspNetCore.Mvc;

namespace Yangtao.Hosting.Mvc.HttpMethods
{
    public class HttpPatchDefaultAttribute : HttpPatchAttribute
    {
        public HttpPatchDefaultAttribute() : base("/api/v{v:ApiVersion}/[controller]")
        {
        }
    }
}
