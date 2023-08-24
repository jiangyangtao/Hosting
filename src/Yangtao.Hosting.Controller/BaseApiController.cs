using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yangtao.Hosting.Controller
{
    [Route("v1/api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public abstract class BaseApiController : HttpResultController
    {
        protected BaseApiController()
        {
        }
    }
}
