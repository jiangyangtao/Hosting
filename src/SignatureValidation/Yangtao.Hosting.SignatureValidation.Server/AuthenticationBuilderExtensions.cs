using Microsoft.AspNetCore.Authentication;
using Yangtao.Hosting.SignatureValidation.Server.Authentication;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddServerSignatureValidationScheme(this AuthenticationBuilder authenticationBuilder)
        {
            authenticationBuilder.AddScheme<AuthenticationSchemeOptions, ServerSignatureValidationAuthenticationHandler>(ServerSignatureValidationDefaultKeys.ServerSignatureValidationScheme, options => { });
            return authenticationBuilder;
        }
    }
}
