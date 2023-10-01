using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data.Common;

namespace Yangtao.Hosting.SqlRepository.Dapper
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
