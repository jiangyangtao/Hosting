using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.SignatureValidation.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSignatureValidationCore(this IServiceCollection services)
        {
            return services;
        }
    }
}
