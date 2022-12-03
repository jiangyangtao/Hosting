using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using Yangtao.Hosting.Cache.Abstractions;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Cache.Redis
{
    internal class RedisProvider : IRedisProvider
    {
        private readonly ILogger<RedisProvider> _logger;
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        private readonly string _prefix;

        public RedisProvider(ILogger<RedisProvider> logger,
            ConnectionMultiplexer connectionMultiplexer, string prefix)
        {
            _logger = logger;
            _prefix = prefix;
            _database = connectionMultiplexer.GetDatabase();
        }

        private IServer _Server;

        private IServer GetServer()
        {
            if (_Server == null)
            {
                var endpoint = _connectionMultiplexer.GetEndPoints();
                _Server = _connectionMultiplexer.GetServer(endpoint.First());
            }

            return _Server;
        }

        public IEnumerable<RedisKey> GetAllKeys() => GetServer().Keys();


        private string BuilderFullKey(string key)
        {
            if (_prefix.IsNullOrEmpty()) return key;

            return $"{_prefix}:{key}";
        }

        public async Task Clear()
        {
            foreach (var endPoint in _connectionMultiplexer.GetEndPoints())
            {
                var server = GetServer();
                foreach (var key in server.Keys())
                {
                    await _database.KeyDeleteAsync(key);
                }
            }
        }

        public async Task<bool> Exist(string key)
        {
            return await _database.KeyExistsAsync(BuilderFullKey(key));
        }

        public async Task<string> GetValue(string key)
        {
            return await _database.StringGetAsync(BuilderFullKey(key));
        }

        public async Task Remove(string key)
        {
            await _database.KeyDeleteAsync(BuilderFullKey(key));
        }

        public async Task Set(string key, object value)
        {
            if (value != null)
            {
                if (value is string cacheValue)
                {
                    // 字符串无需序列化
                    await _database.StringSetAsync(BuilderFullKey(key), cacheValue, TimeSpan.FromDays(7));
                }
                else
                {
                    //序列化，将object值生成RedisValue
                    var jsonValue =
                    await _database.StringSetAsync(BuilderFullKey(key), value.Serialize(), TimeSpan.FromDays(7));
                }
            }
        }

        public async Task Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                if (value is string cacheValue)
                {
                    // 字符串无需序列化
                    await _database.StringSetAsync(BuilderFullKey(key), cacheValue, cacheTime);
                }
                else
                {
                    //序列化，将object值生成RedisValue
                    await _database.StringSetAsync(BuilderFullKey(key), value.Serialize(), cacheTime);
                }
            }
        }

        public async Task<TEntity> Get<TEntity>(string key)
        {
            var fullKey = BuilderFullKey(key);
            var value = await _database.StringGetAsync(fullKey);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return value.ToString().Deserialize<TEntity>();
            }
            else
            {
                return default(TEntity);
            }
        }

        /// <summary>
        /// 根据key获取RedisValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> ListRangeAsync(string redisKey)
        {
            return await _database.ListRangeAsync(BuilderFullKey(redisKey));
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync(string redisKey, string redisValue)
        {
            return await _database.ListLeftPushAsync(BuilderFullKey(redisKey), redisValue);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushObjAsync(string redisKey, object value)
        {
            return await _database.ListLeftPushAsync(BuilderFullKey(redisKey), value.Serialize());
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, string redisValue)
        {
            return await _database.ListRightPushAsync(BuilderFullKey(redisKey), redisValue);
        }

        /// <summary>
        /// 在列表尾部插入数组集合。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, IEnumerable<string> redisValue)
        {
            var redislist = new List<RedisValue>();
            foreach (var item in redisValue)
            {
                redislist.Add(item);
            }
            return await _database.ListRightPushAsync(BuilderFullKey(redisKey), redislist.ToArray());
        }


        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素  反序列化
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string redisKey) where T : class
        {
            var r = await _database.ListLeftPopAsync(BuilderFullKey(redisKey));

            return JsonConvert.DeserializeObject<T>(r);
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素   反序列化
        /// 只能是对象集合
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string redisKey) where T : class
        {
            return JsonConvert.DeserializeObject<T>(await _database.ListRightPopAsync(BuilderFullKey(redisKey)));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素   
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<string> ListLeftPopAsync(string redisKey)
        {
            return await _database.ListLeftPopAsync(BuilderFullKey(redisKey));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素   
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<string> ListRightPopAsync(string redisKey)
        {
            return await _database.ListRightPopAsync(BuilderFullKey(redisKey));
        }

        /// <summary>
        /// 列表长度
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string redisKey)
        {
            return await _database.ListLengthAsync(BuilderFullKey(redisKey));
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListRangeStringAsync(string redisKey)
        {
            var result = await _database.ListRangeAsync(BuilderFullKey(redisKey));
            return result.Select(o => o.ToString());
        }

        /// <summary>
        /// 根据索引获取指定位置数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListRangeStringAsync(string redisKey, int start, int stop)
        {
            var result = await _database.ListRangeAsync(BuilderFullKey(redisKey), start, stop);
            return result.Select(o => o.ToString());
        }

        /// <summary>
        /// 删除List中的元素 并返回删除的个数
        /// </summary>
        /// <param name="redisKey">key</param>
        /// <param name="redisValue">元素</param>
        /// <param name="type">大于零 : 从表头开始向表尾搜索，小于零 : 从表尾开始向表头搜索，等于零：移除表中所有与 VALUE 相等的值</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<long> ListDelRangeAsync(string redisKey, string redisValue, long type = 0)
        {
            return await _database.ListRemoveAsync(BuilderFullKey(redisKey), redisValue, type);
        }

        /// <summary>
        /// 清空List
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        public async Task ListClearAsync(string redisKey)
        {
            await _database.ListTrimAsync(BuilderFullKey(redisKey), 1, 0);
        }


        /// <summary>
        /// 每日流水号生成
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="cacheTime">过期时间</param>
        /// <returns></returns>
        public async Task<long> HashIncrementAsync(string redisKey, string redisValue, TimeSpan cacheTime)
        {
            var value = await _database.HashIncrementAsync(BuilderFullKey(redisKey), redisValue);
            if (value > 0)
            {
                //设置过期时间
                await _database.KeyExpireAsync(BuilderFullKey(redisKey), cacheTime);
                return value;
            }
            else
                return -1;

        }



        /// <summary>
        /// 添加哈希值
        /// </summary>
        /// <param name="key">hash键</param>
        /// <param name="hashFields">要在hash中设置的项</param>
        /// <param name="flags">此操作使用的标志</param>
        /// <returns></returns>
        public async Task HashSetAsync(string key, HashEntry[] hashFields, CommandFlags flags = CommandFlags.None)
        {
            await _database.HashSetAsync(BuilderFullKey(key), hashFields, flags);
        }

        /// <summary>
        /// 添加哈希值
        /// </summary>
        /// <param name="key">hash键</param>
        /// <param name="hashField">要在hash中的字段</param>
        /// <param name="value">要设置的值</param>
        /// <param name="when">在哪些条件下设置字段值(默认为always)。</param>
        /// <param name="flags">此操作使用的标志</param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync(string key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            var r = await _database.HashSetAsync(BuilderFullKey(key), hashField, value, when, flags);
            return r;
        }


        /// <summary>
        /// 获取hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<string> HashGetAsync(string key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
        {
            var hashValue = await _database.HashGetAsync(BuilderFullKey(key), hashField, flags);
            if (hashValue.IsNull) return string.Empty;

            return hashValue.ToString();
        }
    }
}
