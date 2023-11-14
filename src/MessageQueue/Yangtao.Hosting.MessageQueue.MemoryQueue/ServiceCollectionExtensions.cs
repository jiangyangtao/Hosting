using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.MessageQueue.MemoryQueue
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services)
        {
            return services;
        }
    }
}