using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.GrpcCore.Abstractions;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcCore.SignProviders
{
    internal class SignProviderFactory
    {
        private readonly IOptions<SignAuthenticationOptions> _signOptions;
        private readonly IOptionsMonitor<SignAuthenticationOptions> _signOptionsMonitor;
        private readonly IServiceProvider _serviceProvider;

        public SignProviderFactory(
            IOptions<SignAuthenticationOptions> signOptions,
            IOptionsMonitor<SignAuthenticationOptions> signOptionsMonitor,
            IServiceProvider serviceProvider)
        {
            _signOptions = signOptions;
            _signOptionsMonitor = signOptionsMonitor;
            _serviceProvider = serviceProvider;
        }

        public ISignProvider CreateSignProvider(string? optionName = "")
        {
            var services = _serviceProvider.GetServices<ISignProvider>();
            var signAuthenticationType = GetSignAuthenticationType(optionName);
            return services.FirstOrDefault(a => a.SignAuthenticationType == signAuthenticationType) ?? throw new NotImplementedException(nameof(SignAuthenticationType));
        }

        private SignAuthenticationType GetSignAuthenticationType(string? optionName)
        {
            if (optionName.NotNullAndEmpty())
            {
                var options = _signOptionsMonitor.Get(optionName) ?? throw new KeyNotFoundException(optionName);
                if (options.SignAuthenticationType.HasValue == false) throw new NullReferenceException(nameof(SignAuthenticationOptions.SignAuthenticationType));

                return options.SignAuthenticationType.Value;
            }

            if (_signOptions.Value.SignAuthenticationType.HasValue == false) throw new NullReferenceException(nameof(SignAuthenticationOptions.SignAuthenticationType));
            return _signOptions.Value.SignAuthenticationType.Value;
        }
    }
}
