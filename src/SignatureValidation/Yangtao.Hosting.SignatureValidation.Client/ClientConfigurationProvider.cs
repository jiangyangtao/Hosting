using Microsoft.Extensions.Options;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    internal class ClientConfigurationProvider : IClientConfigurationProvider
    {
        private readonly IOptions<ClientValidationOptions> _clientValidationOptions;
        private readonly IOptions<HmacShaOptions> _hmacShaOptions;
        private readonly IOptions<RsaSignatureOptions> _rsaSignatureOptions;
        private readonly IOptions<RsaEncryptionOptions> _rsaEncryptionOptions;

        public ClientConfigurationProvider(
            IOptions<ClientValidationOptions> clientValidationOptions,
            IOptions<HmacShaOptions> hmacShaOptions,
            IOptions<RsaSignatureOptions> rsaSignatureOptions,
            IOptions<RsaEncryptionOptions> rsaEncryptionOptions)
        {
            _clientValidationOptions = clientValidationOptions;
            _hmacShaOptions = hmacShaOptions;
            _rsaSignatureOptions = rsaSignatureOptions;
            _rsaEncryptionOptions = rsaEncryptionOptions;
        }

        public ClientValidationOptions ClientValidationOptions => _clientValidationOptions.Value;

        public HmacShaOptions? HmacShaOptions => _hmacShaOptions.Value ?? null;

        public RsaSignatureOptions? RsaSignatureOptions => _rsaSignatureOptions.Value ?? null;

        public RsaEncryptionOptions? RsaEncryptionOptions => _rsaEncryptionOptions.Value ?? null;
    }
}
