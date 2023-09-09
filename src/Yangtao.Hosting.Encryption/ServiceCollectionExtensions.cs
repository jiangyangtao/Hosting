using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Encryption.AdvancedEncryptionStandard;
using Yangtao.Hosting.Encryption.TripleDataEncryptionAlgorithm;

namespace Yangtao.Hosting.Encryption
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAesEncryption(this IServiceCollection services, Action<EncryptionOptions> actionOptions)
        {
            var options = new EncryptionOptions();
            actionOptions(options);

            services.AddSingleton(new AesProvider(options.SecretKey, options.Iv));
            return services;
        }

        public static IServiceCollection AddTripleDesEncryption(this IServiceCollection services, Action<EncryptionOptions> actionOptions)
        {
            var options = new EncryptionOptions();
            actionOptions(options);

            services.AddSingleton(new TripleDesProvider(options.SecretKey, options.Iv));
            return services;
        }
    }
}
