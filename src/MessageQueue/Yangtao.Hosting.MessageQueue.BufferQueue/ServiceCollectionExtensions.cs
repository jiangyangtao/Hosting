using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yangtao.Hosting.MessageQueue.BufferQueue.Abstracts;

namespace Yangtao.Hosting.MessageQueue.BufferQueue
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBufferQueue(this IServiceCollection services)
        {
            services.AddSingleton<BufferQueueProvider>();
            services.AddSingleton<IBufferQueueProvider, BufferQueueProvider>();
            services.AddSingleton<IHostedService, BufferQueueProvider>();
            return services;
        }
    }
}
