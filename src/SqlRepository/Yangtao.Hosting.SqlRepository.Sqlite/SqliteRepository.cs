using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System.Data.Common;
using Yangtao.Hosting.SqlRepository.Core;

namespace Yangtao.Hosting.SqlRepository.Sqlite
{
    internal class SqliteRepository : SqlRepositoryBase
    {
        private readonly SqlRepositoryOptions RepositoryOptions;

        public SqliteRepository(IOptions<SqlRepositoryOptions> options)
        {
            RepositoryOptions = options.Value;
        }

        protected override DbConnection GetDbConnection()
        {
            var sqliteConnection = new SqliteConnection(RepositoryOptions.ConnectionsString);
            sqliteConnection.Open();

            return sqliteConnection;
        }

        protected override async Task<DbConnection> GetDbConnectionAsync()
        {
            var sqliteConnection = new SqliteConnection(RepositoryOptions.ConnectionsString);
            await sqliteConnection.OpenAsync();

            return sqliteConnection;
        }
    }
}
