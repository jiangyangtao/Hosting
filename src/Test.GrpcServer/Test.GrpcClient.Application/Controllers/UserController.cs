using Microsoft.AspNetCore.Mvc;
using Test.GrpcServer.Provider;

namespace Test.GrpcClient.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly GrpcValidationProvider.GrpcValidationProviderClient _providerClient;

        public UserController(GrpcValidationProvider.GrpcValidationProviderClient providerClient)
        {
            _providerClient = providerClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var r = await _providerClient.LoginAsync(new LoginRequest
            {
                Username = "11111111",
                Passwrod = "222222222",
            });
            return Ok(r.AccessToken);
        }
    }
}
