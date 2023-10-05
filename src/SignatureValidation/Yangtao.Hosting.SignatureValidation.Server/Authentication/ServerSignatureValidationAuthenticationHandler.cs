using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server.Authentication
{
    internal class ServerSignatureValidationAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly AuthenticateResult EmptyAuthenticateSuccessResult;

        public ServerSignatureValidationAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(), ServerSignatureValidationDefaultKeys.ServerSignatureValidationScheme);
            EmptyAuthenticateSuccessResult = AuthenticateResult.Success(ticket);
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var hasSignatureKey = Context.Request.Headers.TryGetValue(SignatureValidationDefaultKeys.SignatureKey, out StringValues value);
            if (hasSignatureKey == false) AuthenticateResult.NoResult();

            return Task.FromResult(EmptyAuthenticateSuccessResult);
        }
    }
}
