using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Text.Encodings.Web;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server.Authentication
{
    internal class ServerSignatureValidationAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public ServerSignatureValidationAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) 
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Context.Request.Headers.TryGetValue(SignatureValidationDefaultKeys.SignatureKey, out StringValues value);
            throw new NotImplementedException();
        }
    }
}
