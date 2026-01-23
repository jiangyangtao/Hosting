using Yangtao.Hosting.GrpcCore.Abstractions;

namespace Yangtao.Hosting.GrpcCore.SignProviders
{
    internal class SignAuthenticationProvider : ISignAuthenticationProvider
    {
        private readonly SignProviderFactory _signProviderFactory;

        public SignAuthenticationProvider(SignProviderFactory signProviderFactory)
        {
            _signProviderFactory = signProviderFactory;
        }

        public string Decrypt(string value, string? optionName = "") => _signProviderFactory.CreateSignProvider().Decrypt(value, optionName);

        public string Encrypt(string value, string? optionName) => _signProviderFactory.CreateSignProvider().Encrypt(value, optionName);
    }
}
