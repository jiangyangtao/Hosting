
namespace Yangtao.Hosting.Cache.Redis.Abstracts
{
    public interface IRedisCacheContainer
    {
        /// <summary>
        /// 创建一个 Redis 缓存提供者
        /// </summary>
        /// <param name="name">缓存容器名称</param>
        /// <returns>缓存容器</returns>
        IRedisCacheProvider CreateRedisCacheProvider(string name = "");

        /// <summary>
        /// 容器名
        /// </summary>
        string Name { get; }
    }
}
