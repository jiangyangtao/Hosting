using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.SignatureValidation.Core.Abstractions;

namespace Yangtao.Hosting.SignatureValidation.Core.HmacShaPorviders
{
    internal class HmacShaPorviderFactory : IHmacShaPorviderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISignatureValidationConfigurationProvider _configurationProvider;

        public HmacShaPorviderFactory(
            IServiceProvider serviceProvider,
            ISignatureValidationConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            _serviceProvider = serviceProvider;
        }

        public IHmacShaProvider CreateIHmacShaProvider()
        {
            var hmacShaConfiguration = _configurationProvider.GetHmacShaConfiguration();
            var provider = _serviceProvider.GetServices<IHmacShaProvider>().FirstOrDefault(a => a.HmacShaAlgorithmType == hmacShaConfiguration.HmacShaAlgorithmType);

            return provider ?? throw new KeyNotFoundException($"Not found {hmacShaConfiguration.HmacShaAlgorithmType} the HmacShaProvider.");
        }
    }
}
