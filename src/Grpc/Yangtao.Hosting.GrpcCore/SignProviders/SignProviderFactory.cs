using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Yangtao.Hosting.GrpcCore.Abstractions;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcCore.SignProviders
{
    internal class SignProviderFactory
    {
        private readonly IOptions<SignAuthenticationOptions> _signOptions;
        private readonly IServiceProvider _serviceProvider;

        public SignProviderFactory(IOptions<SignAuthenticationOptions> signOptions, IServiceProvider serviceProvider)
        {
            _signOptions = signOptions;
            _serviceProvider = serviceProvider;
        }

        public ISignProvider CreateSignProvider()
        {
            var services = _serviceProvider.GetServices<ISignProvider>();
            return services.FirstOrDefault(a => a.SignAuthenticationType == _signOptions.Value.SignAuthenticationType);
        }
    }
}
