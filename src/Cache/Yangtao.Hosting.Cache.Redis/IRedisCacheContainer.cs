using Yangtao.Hosting.Cache.Abstractions;

namespace Yangtao.Hosting.Cache.Redis
{
    public interface IRedisCacheContainer : ICacheContainer
    {
        /// <summary>
        /// 创建一个 Redis 缓存提供者
        /// </summary>
        /// <param name="name">缓存容器名称</param>
        /// <returns>缓存容器</returns>
        IRedisProvider CreateRedisProvider(string name = "");
    }
}
