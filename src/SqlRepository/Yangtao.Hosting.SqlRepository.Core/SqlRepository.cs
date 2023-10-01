using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data.Common;
using Yangtao.Hosting.SqlRepository.Abstractions;

namespace Yangtao.Hosting.SqlRepository.Core
{
    internal class SqlRepository : ISqlRepository
    {
        private readonly SqlRepositoryOptions _dapperOptions;

        public SqlRepository(IOptions<SqlRepositoryOptions> options)
        {
            _dapperOptions = options.Value;
        }

        private DbConnection GetDbConnection()
        {
            var connection = new MySqlConnection(_dapperOptions.DbConnectionString);
            connection.Open();
            return connection;
        }
    }
}
