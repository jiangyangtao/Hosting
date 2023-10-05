using Microsoft.AspNetCore.Authorization;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server.Attributes
{
    public class ServerSignatureValidationAttribute : AuthorizeAttribute
    {
        public ServerSignatureValidationAttribute() : base(ServerSignatureValidationDefaultKeys.ServerSignatureValidationScheme)
        {
        }
    }
}
