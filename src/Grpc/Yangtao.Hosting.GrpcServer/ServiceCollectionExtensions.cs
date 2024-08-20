using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.GrpcCore;
using Yangtao.Hosting.GrpcCore.Options;
using Yangtao.Hosting.GrpcServer.Interceptors;
using Yangtao.Hosting.GrpcServer.Options;

namespace Yangtao.Hosting.GrpcServer
{
    /// <summary>
    /// <see cref="IServiceCollection"/> Extension for GrpcServer
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 Grpc Server
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IServiceCollection AddGrpcServer(this IServiceCollection services, Action<GrpcServerOptions> action)
        {
            var serverOptions = new GrpcServerOptions();
            action(serverOptions);

            services.AddGrpc(options =>
             {
                 options.CompressionProviders = serverOptions.GrpcServiceOptions.CompressionProviders;
                 options.ResponseCompressionAlgorithm = serverOptions.GrpcServiceOptions.ResponseCompressionAlgorithm;

                 if (serverOptions.GrpcServiceOptions.MaxSendMessageSize.HasValue)
                     options.MaxSendMessageSize = serverOptions.GrpcServiceOptions.MaxSendMessageSize;

                 if (serverOptions.GrpcServiceOptions.MaxReceiveMessageSize.HasValue)
                     options.MaxReceiveMessageSize = serverOptions.GrpcServiceOptions.MaxReceiveMessageSize;

                 if (serverOptions.GrpcServiceOptions.EnableDetailedErrors.HasValue)
                     options.EnableDetailedErrors = serverOptions.GrpcServiceOptions.EnableDetailedErrors;

                 if (serverOptions.GrpcServiceOptions.ResponseCompressionLevel.HasValue)
                     options.ResponseCompressionLevel = serverOptions.GrpcServiceOptions.ResponseCompressionLevel;

                 if (serverOptions.GrpcServiceOptions.IgnoreUnknownServices.HasValue)
                     options.IgnoreUnknownServices = serverOptions.GrpcServiceOptions.IgnoreUnknownServices;

                 if (serverOptions.GrpcServiceOptions.Interceptors.NotNullAndEmpty())
                 {
                     foreach (var interceptor in serverOptions.GrpcServiceOptions.Interceptors)
                     {
                         options.Interceptors.Add(interceptor);
                     }
                 }

                 options.Interceptors.Add<GrpcHttpErrorHandleInterceptor>();


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