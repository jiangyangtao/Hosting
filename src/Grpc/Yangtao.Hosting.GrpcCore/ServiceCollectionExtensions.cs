using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yangtao.Hosting.GrpcCore.Abstractions;
using Yangtao.Hosting.GrpcCore.Options;
using Yangtao.Hosting.GrpcCore.SignProviders;

namespace Yangtao.Hosting.GrpcCore
{
    /// <summary>
    /// <see cref="IServiceCollection"/> Extensions for GrpcCore
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 Grpc 核心组件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddGrpcCore(this IServiceCollection services, Action<SignAuthenticationOptions> optionAction)
        {
            var option = new SignAuthenticationOptions();
            optionAction(option);

            services.Configure<SignAuthenticationOptions>(option.OptionName, a =>
            {
                a.SignAuthenticationType = option.SignAuthenticationType;
                a.AesSignOptions = option.AesSignOptions;
                a.RsaPrivateSignOptions = option.RsaPrivateSignOptions;
                a.RsaPublicSignOptions = option.RsaPublicSignOptions;
            });

            services.TryAddSingleton<SignProviderFactory>();
            services.TryAddSingleton<ISignProvider, AesSignProvider>();
            services.TryAddSingleton<ISignProvider, RsaSignProvider>();
            services.TryAddSingleton<ISignAuthenticationProvider, SignAuthenticationProvider>();

            return services;
        }
    }
}
