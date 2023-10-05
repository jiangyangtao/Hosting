using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

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
          
            throw new NotImplementedException();
        }
    }
}
