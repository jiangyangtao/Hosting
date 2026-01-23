using Microsoft.Extensions.Options;
using Yangtao.Hosting.Encryption.RsaAlgorithm;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.GrpcCore.Abstractions;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcCore.SignProviders
{
    internal class RsaSignProvider : ISignProvider
    {
        private readonly IOptions<SignAuthenticationOptions> _signOptions;
        private readonly IOptionsMonitor<SignAuthenticationOptions> _signOptionsMonitor;

        public RsaSignProvider(IOptions<SignAuthenticationOptions> signOptions, IOptionsMonitor<SignAuthenticationOptions> signOptionsMonitor)
        {
            _signOptions = signOptions;
            _signOptionsMonitor = signOptionsMonitor;
        }

        public SignAuthenticationType SignAuthenticationType => SignAuthenticationType.RSA;

        private RsaPrivateSignOptions GetRsaPrivateOptions(string? optionName)
        {
            if (optionName.NotNullAndEmpty())
            {
                var options = _signOptionsMonitor.Get(optionName) ?? throw new KeyNotFoundException(optionName);
                if (options.RsaPrivateSignOptions == null) throw new NullReferenceException(nameof(SignAuthenticationOptions.RsaPrivateSignOptions));

                return options.RsaPrivateSignOptions;
            }

            if (_signOptions.Value.RsaPrivateSignOptions == null) throw new NullReferenceException(nameof(SignAuthenticationOptions.RsaPrivateSignOptions));
            return _signOptions.Value.RsaPrivateSignOptions;
        }

        private RsaPublicSignOptions GetRsaPublicOptions(string? optionName)
        {
            if (optionName.NotNullAndEmpty())
            {
                var options = _signOptionsMonitor.Get(optionName) ?? throw new KeyNotFoundException(optionName);
                if (options.RsaPublicSignOptions == null) throw new NullReferenceException(nameof(SignAuthenticationOptions.RsaPrivateSignOptions));

                return options.RsaPublicSignOptions;
            }

            if (_signOptions.Value.RsaPublicSignOptions == null) throw new NullReferenceException(nameof(SignAuthenticationOptions.RsaPrivateSignOptions));
            return _signOptions.Value.RsaPublicSignOptions;
        }

        public string Decrypt(string value, string? optionName = "")
        {
            var rsaPrivateOptions = GetRsaPrivateOptions(optionName);
            using var rsaPrivate = new RsaPrivate(rsaPrivateOptions.PrivateKey, rsaPrivateOptions.RSAKeyType);
            return rsaPrivate.Decrypt(value);
        }

        public string Encrypt(string value, string? optionName)
        {
            var rsaPublicOptions = GetRsaPublicOptions(optionName);
            using var raPublic = new RsaPublic(rsaPublicOptions.PublicKey, rsaPublicOptions.RSAKeyType);
            return raPublic.Encrypt(value);
        }
    }
}
