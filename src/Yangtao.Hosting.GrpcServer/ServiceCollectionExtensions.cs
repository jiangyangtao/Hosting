using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.GrpcCore.Options;
using Yangtao.Hosting.GrpcCore;
using Yangtao.Hosting.GrpcServer.Interceptors;
using Yangtao.Hosting.GrpcServer.Options;

namespace Yangtao.Hosting.GrpcServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGrpcServer(this IServiceCollection services, Action<GrpcServerOptions> action)
        {
            var serverOptions = new GrpcServerOptions();
            services.AddGrpc(options =>
             {
                 serverOptions.GrpcServiceOptions = options;
                 options.Interceptors.Add<GrpcHttpErrorHandleInterceptor>();
                 action(serverOptions);

                 if (serverOptions.SignAuthenticationType.HasValue)
                     options.Interceptors.Add<SignAuthenticationGrpcServerInterceptor>();
             });


            if (serverOptions.SignAuthenticationType.HasValue)
            {
                if (serverOptions.SignAuthenticationType == SignAuthenticationType.Aes && serverOptions.AesSignOptions == null)
                    throw new ArgumentException(nameof(serverOptions.AesSignOptions));

                if (serverOptions.SignAuthenticationType == SignAuthenticationType.RSA && serverOptions.RsaPrivateSignOptions == null)
                    throw new ArgumentException(nameof(serverOptions.RsaPrivateSignOptions));

                services.AddGrpcCore(options =>
                {
                    options.SignAuthenticationType = serverOptions.SignAuthenticationType;
                    options.AesSignOptions = serverOptions.AesSignOptions;
                    options.RsaPrivateSignOptions = serverOptions.RsaPrivateSignOptions;
                });
            }

            return services;
        }
    }
}