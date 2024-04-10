using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.GrpcCore.Abstractions;
using Yangtao.Hosting.GrpcCore.Options;
using Yangtao.Hosting.GrpcCore.SignProviders;

namespace Yangtao.Hosting.GrpcCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGrpcCore(this IServiceCollection services, Action<SignAuthenticationOptions> optionAction)
        {
            var option = new SignAuthenticationOptions();
            optionAction(option);

            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            services.Configure<SignAuthenticationOptions>(a =>
            {
                a.SignAuthenticationType = option.SignAuthenticationType;
                a.AesSignOptions = option.AesSignOptions;
                a.RsaPrivateSignOptions = option.RsaPrivateSignOptions;
                a.RsaPublicSignOptions = option.RsaPublicSignOptions;
            });

            services.AddSingleton<SignProviderFactory>();
            services.AddSingleton<ISignProvider, AesSignProivder>();
            services.AddSingleton<ISignProvider, RsaSignProvider>();
            services.AddSingleton<ISignAuthenticationProvider, SignAuthenticationProvider>();

            return services;
        }
    }
}
