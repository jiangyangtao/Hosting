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
        private readonly IOptions<RsaOptions> _rsaOptions;

        public ClientConfigurationProvider(
            IOptions<ClientValidationOptions> clientValidationOptions,
            IOptions<HmacShaOptions> hmacShaOptions,
            IOptions<RsaOptions> rsaOptions)
        {
            _clientValidationOptions = clientValidationOptions;
            _hmacShaOptions = hmacShaOptions;
            _rsaOptions = rsaOptions;
        }

        public ClientValidationOptions ClientValidationOptions => _clientValidationOptions.Value;

        public RsaOptions? RsaPublicOptions => _rsaOptions.Value ?? null;

        public HmacShaOptions? HmacShaOptions => _hmacShaOptions.Value ?? null;
    }
}
