using Microsoft.Extensions.Logging;
using Yangtao.Hosting.GrpcCore.Abstractions;

namespace Yangtao.Hosting.GrpcCore.SignProviders
{
    internal class SignAuthenticationProvider : ISignAuthenticationProvider
    {
        private readonly SignProviderFactory _signProviderFactory;
        private readonly ILogger<SignAuthenticationProvider> _logger;

        public SignAuthenticationProvider(SignProviderFactory signProviderFactory, ILogger<SignAuthenticationProvider> logger)
        {
            _signProviderFactory = signProviderFactory;
            _logger = logger;
        }

        public string Decrypt(string value, string? optionName = "")
        {
            var signProvider = _signProviderFactory.CreateSignProvider(optionName);
            _logger.LogDebug("Use {SignAuthenticationType} sign decrypt.", signProvider.SignAuthenticationType);

            return _signProviderFactory.CreateSignProvider(optionName).Decrypt(value, optionName);
        }

        public string Encrypt(string value, string? optionName)
        {
            var signProvider = _signProviderFactory.CreateSignProvider(optionName);
            _logger.LogDebug("Use {SignAuthenticationType} sign encrypt.", signProvider.SignAuthenticationType);

            return _signProviderFactory.CreateSignProvider(optionName).Encrypt(value, optionName);
        }
    }
}
