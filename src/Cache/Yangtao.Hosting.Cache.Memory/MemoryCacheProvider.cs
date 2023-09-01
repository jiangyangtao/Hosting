using Microsoft.Extensions.Caching.Memory;
using Yangtao.Hosting.Cache.Memory.Abstracts;

namespace Yangtao.Hosting.Cache.Memory
{
    internal class MemoryCacheProvider : IMemoryCacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IMemoryCache MemoryCache => _memoryCache;

        public async Task<string> GetAsync(string key)
        {
            var data = _memoryCache.Get<string>(key);
            if (data == null) return string.Empty;

            return await Task.FromResult(data);
        }

        public Task<T?> GetAsync<T>(string key)
        {
            var data = _memoryCache.Get<T>(key);
            return Task.FromResult(data);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task SetAsync(string key, string data, DateTimeOffset absoluteExpiration)
        {
            _memoryCache.Set(key, data, absoluteExpiration);
            return Task.CompletedTask;
        }

        public Task SetAsync<T>(string key, T data, DateTimeOffset absoluteExpiration)
        {
            _memoryCache.Set(key, data, absoluteExpiration);
            return Task.CompletedTask;
        }
    }
}
