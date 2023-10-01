using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data.Common;
using Yangtao.Hosting.SqlRepository.Core;

namespace Yangtao.Hosting.SqlRepository.MySql
{
    internal class MySqlRepository : SqlRepositoryBase
    {
        private readonly SqlRepositoryOptions RepositoryOptions;

        public MySqlRepository(IOptions<SqlRepositoryOptions> options)
        {
            RepositoryOptions = options.Value;
        }

        protected override DbConnection GetDbConnection()
        {
            var mysqlConnection = new MySqlConnection(RepositoryOptions.ConnectionsString);
            mysqlConnection.Open();

            return mysqlConnection;
        }

        protected override async Task<DbConnection> GetDbConnectionAsync()
        {
            var mysqlConnection = new MySqlConnection(RepositoryOptions.ConnectionsString);
            await mysqlConnection.OpenAsync();

            return mysqlConnection;
        }
    }
}
