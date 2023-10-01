using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data.Common;
using Yangtao.Hosting.SqlRepository.Core;

namespace Yangtao.Hosting.SqlRepository.SqlServer
{
    internal class SqlServerRepository : SqlRepositoryBase
    {
        private readonly SqlRepositoryOptions RepositoryOptions;

        public SqlServerRepository(IOptions<SqlRepositoryOptions> options)
        {
            RepositoryOptions = options.Value;
        }

        protected override DbConnection GetDbConnection()
        {
            var sqlConnection = new SqlConnection();
            sqlConnection.Open();

            return sqlConnection;
        }

        protected override async Task<DbConnection> GetDbConnectionAsync()
        {
            var sqlConnection = new SqlConnection();
            await sqlConnection.OpenAsync();

            return sqlConnection;
        }
    }
}
