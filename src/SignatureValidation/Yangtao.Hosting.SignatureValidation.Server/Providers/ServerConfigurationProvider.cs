using Microsoft.Extensions.Options;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server.Providers
{
    internal class ServerConfigurationProvider : IServerConfigurationProvider
    {
        private readonly IOptions<ServerValidationOptions> _serverValidationOptions;
        private readonly IOptions<HmacShaOptions> _hmacShaOptions;
        private readonly IOptions<RsaSignatureOptions> _rsaSignatureOptions;
        private readonly IOptions<RsaEncryptionOptions> _rsaEncryptionOptions;

        public ServerConfigurationProvider(IOptions<ServerValidationOptions> serverValidationOptions,
            IOptions<HmacShaOptions> hmacShaOptions,
            IOptions<RsaSignatureOptions> rsaSignatureOptions,
            IOptions<RsaEncryptionOptions> rsaEncryptionOptions)
        {
            _serverValidationOptions = serverValidationOptions;
            _hmacShaOptions = hmacShaOptions;
            _rsaSignatureOptions = rsaSignatureOptions;
            _rsaEncryptionOptions = rsaEncryptionOptions;
        }

        public ServerValidationOptions ServerValidationOptions => _serverValidationOptions.Value;

        public HmacShaOptions? HmacShaOptions => _hmacShaOptions.Value ?? null;

        public RsaSignatureOptions? RsaSignatureOptions => _rsaSignatureOptions.Value ?? null;

        public RsaEncryptionOptions? RsaEncryptionOptions => _rsaEncryptionOptions.Value ?? null;
    }
}
