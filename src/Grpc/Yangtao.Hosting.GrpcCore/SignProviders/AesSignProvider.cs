using Microsoft.Extensions.Options;
using Yangtao.Hosting.Encryption.AdvancedEncryptionStandard;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.GrpcCore.Abstractions;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcCore.SignProviders
{
    internal class AesSignProvider : ISignProvider
    {
        private readonly IOptions<SignAuthenticationOptions> _signOptions;
        private readonly IOptionsMonitor<SignAuthenticationOptions> _signOptionsMonitor;

        public AesSignProvider(IOptions<SignAuthenticationOptions> signOptions, IOptionsMonitor<SignAuthenticationOptions> signOptionsMonitor)
        {
            _signOptions = signOptions;
            _signOptionsMonitor = signOptionsMonitor;
        }

        public SignAuthenticationType SignAuthenticationType => SignAuthenticationType.Aes;

        private AesSignOptions GetAesSignOptions(string? optionName)
        {
            if (optionName.NotNullAndEmpty())
            {
                var options = _signOptionsMonitor.Get(optionName) ?? throw new KeyNotFoundException(optionName);
                if (options.AesSignOptions == null) throw new NullReferenceException(nameof(SignAuthenticationOptions.AesSignOptions));

                return options.AesSignOptions;
            }

            if (_signOptions.Value.AesSignOptions == null) throw new NullReferenceException(nameof(SignAuthenticationOptions.AesSignOptions));
            return _signOptions.Value.AesSignOptions;
        }

        public string Decrypt(string value, string? optionName = "")
        {
            var aesOptions = GetAesSignOptions(optionName);
            using var aes = new Aes(aesOptions.SecurityKey, aesOptions.Iv);
            return aes.Decrypt(value);
        }

        public string Encrypt(string value, string? optionName)
        {
            var aesOptions = GetAesSignOptions(optionName);
            using var aes = new Aes(aesOptions.SecurityKey, aesOptions.Iv);
            return aes.Encrypt(value);
        }
    }
}
