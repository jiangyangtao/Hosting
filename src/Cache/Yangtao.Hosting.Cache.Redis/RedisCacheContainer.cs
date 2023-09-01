using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Yangtao.Hosting.Cache.Redis.Abstracts;

namespace Yangtao.Hosting.Cache.Redis
{
    internal class RedisCacheContainer : IRedisCacheContainer
    {
        private readonly string _connectionString;
        private readonly ILogger<RedisCacheProvider> _logger;


        public RedisCacheContainer(IServiceProvider serviceProvider, string connectionString)
        {
            _connectionString = connectionString;
            _logger = serviceProvider.GetRequiredService<ILogger<RedisCacheProvider>>();
        }

        public string Name { set; get; }

        public IRedisCacheProvider CreateRedisCacheProvider(string name = "")
        {
            Name = name;
            return new RedisCacheProvider(_logger, ConnectionMultiplexer, name);
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
