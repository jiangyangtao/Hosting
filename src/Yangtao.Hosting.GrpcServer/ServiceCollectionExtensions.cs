﻿using Grpc.AspNetCore.Server;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.GrpcServer.Interceptors;

namespace Yangtao.Hosting.GrpcServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGrpcServer(this IServiceCollection services, Action<GrpcServiceOptions> action)
        {
            services.AddGrpc(options =>
            {
                options.Interceptors.Add<GrpcHttpErrorHandleInterceptor>();
                action(options);
            });

            return services;
        }
    }
}