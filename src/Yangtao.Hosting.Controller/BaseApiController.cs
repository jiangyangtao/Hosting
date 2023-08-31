using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yangtao.Hosting.Controller
{
    [Route("api/v{v:ApiVersion}/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public abstract class BaseApiController : HttpResultController
    {
        protected BaseApiController()
        {
        }
    }
}
