using StackExchange.Redis;

namespace Yangtao.Hosting.Cache.Abstractions
{
    public interface IRedisProvider
    {

        IEnumerable<RedisKey> GetAllKeys();

        /// <summary>
        /// 获取 Reids 缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetValue(string key);

        /// <summary>
        /// 获取值，并序列化
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TEntity> Get<TEntity>(string key);


        /// <summary>
        /// 设置一个值，默认7天有效期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task Set(string key, object value);

        /// <summary>
        /// 设置一个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        Task Set(string key, object value, TimeSpan cacheTime);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Exist(string key);

        /// <summary>
        /// 移除某一个缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task Remove(string key);

        ///// <summary>
        ///// 全部清除
        ///// </summary>
        ///// <returns></returns>
        //Task Clear();

        //Task<RedisValue[]> ListRangeAsync(string redisKey);

        Task<long> ListLeftPushAsync(string redisKey, string redisValue);

        Task<long> ListLeftPushObjAsync(string redisKey, object value);

        Task<long> ListRightPushAsync(string redisKey, string redisValue);

        Task<long> ListRightPushAsync(string redisKey, IEnumerable<string> redisValue);

        Task<T> ListLeftPopAsync<T>(string redisKey) where T : class;

        Task<T> ListRightPopAsync<T>(string redisKey) where T : class;

        Task<string> ListLeftPopAsync(string redisKey);

        Task<string> ListRightPopAsync(string redisKey);

        Task<long> ListLengthAsync(string redisKey);

        Task<IEnumerable<string>> ListRangeStringAsync(string redisKey);

        Task<IEnumerable<string>> ListRangeStringAsync(string redisKey, int start, int stop);

        Task<long> ListDelRangeAsync(string redisKey, string redisValue, long type = 0);

        Task ListClearAsync(string redisKey);

        

        #region 哈希表操作

        Task<long> HashIncrementAsync(string redisKey, string redisValue, TimeSpan cacheTime);

        /// <summary>
        /// 添加哈希值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        Task HashSetAsync(string key, HashEntry[] hashFields, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// 添加哈希值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        Task<bool> HashSetAsync(string key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// 获取hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<string> HashGetAsync(string key, RedisValue hashField, CommandFlags flags = CommandFlags.None);

        #endregion
    }
}
