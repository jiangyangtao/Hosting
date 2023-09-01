using Microsoft.Extensions.Caching.Memory;

namespace Yangtao.Hosting.Cache.Memory.Abstracts
{
    public interface IMemoryCacheProvider
    {
        IMemoryCache MemoryCache { get; }

        Task<string> GetAsync(string key);

        Task<T?> GetAsync<T>(string key);

        Task SetAsync(string key, string data, DateTimeOffset absoluteExpiration);

        Task SetAsync<T>(string key, T data, DateTimeOffset absoluteExpiration);

        Task RemoveAsync(string key);
    }
}
