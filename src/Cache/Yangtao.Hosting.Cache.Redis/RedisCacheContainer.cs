using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Yangtao.Hosting.Cache.Redis.Abstracts;

namespace Yangtao.Hosting.Cache.Redis
{
    internal class RedisCacheContainer : IRedisCacheContainer
    {
        private readonly string _connectionString;
        private readonly ILogger<RedisProvider> _logger;


        public RedisCacheContainer(IServiceProvider serviceProvider, string connectionString)
        {
            _connectionString = connectionString;
            _logger = serviceProvider.GetRequiredService<ILogger<RedisProvider>>();
        }

        public string Name { set; get; }

        public IRedisProvider CreateRedisProvider(string name = "")
        {
            Name = name;
            return new RedisProvider(_logger, ConnectionMultiplexer, name);
        }

        private ConnectionMultiplexer ConnectionMultiplexer
        {
            get
            {
                var configuration = ConfigurationOptions.Parse(_connectionString, true);
                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration);
            }
        }
    }
}
