using Grpc.AspNetCore.Server;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.GrpcServer.Interceptors;

namespace Yangtao.Hosting.GrpcServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGrpcServer(this ServiceCollection services, Action<GrpcServiceOptions> action)
        {
            services.AddGrpc(options =>
            {
                options.Interceptors.Add<GrpcGlobalInterceptor>();
                action(options);
            });

            return services;
        }
    }
}