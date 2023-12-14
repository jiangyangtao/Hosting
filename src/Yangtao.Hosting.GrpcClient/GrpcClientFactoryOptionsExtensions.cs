﻿using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Grpc.Net.ClientFactory;

namespace Yangtao.Hosting.GrpcClient
{
    public static class GrpcClientFactoryOptionsExtensions
    {
        public static GrpcClientFactoryOptions SetDefault(this GrpcClientFactoryOptions grpcClientFactoryOptions, GrpcClientOptions clientOptions)
        {
            return grpcClientFactoryOptions.SetDefault(grpcClientOptions => grpcClientOptions = clientOptions);
        }

        public static GrpcClientFactoryOptions SetDefault(this GrpcClientFactoryOptions grpcClientFactoryOptions, Action<GrpcClientOptions> optionAction)
        {
            var clientOptions = new GrpcClientOptions();
            optionAction(clientOptions);

            grpcClientFactoryOptions.Address = new Uri(clientOptions.Endpoint);
            grpcClientFactoryOptions.ChannelOptionsActions.Add((channelOptions) =>
            {
                if (clientOptions.AllowAnyServerCertificate)
                {
                    // 允许自签名证书
                    channelOptions.HttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                    };
                }

                if (clientOptions.AddRetry)
                {
                    var serviceConfig = new ServiceConfig();
                    serviceConfig.MethodConfigs.Add(new MethodConfig
                    {
                        Names = { MethodName.Default },
                        RetryPolicy = new RetryPolicy       // 重试策略
                        {
                            MaxAttempts = 5,
                            InitialBackoff = TimeSpan.FromSeconds(1),
                            MaxBackoff = TimeSpan.FromSeconds(5),
                            BackoffMultiplier = 1.5,
                            RetryableStatusCodes = { StatusCode.Unavailable }
                        }
                    });
                    channelOptions.ServiceConfig = serviceConfig;
                }
            });

            return grpcClientFactoryOptions;
        }
    }
}