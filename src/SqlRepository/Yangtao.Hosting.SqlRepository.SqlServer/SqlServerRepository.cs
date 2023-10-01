using System.Data.Common;
using Yangtao.Hosting.SqlRepository.Core;

namespace Yangtao.Hosting.SqlRepository.SqlServer
{
    internal class SqlServerRepository : SqlRepositoryBase
    {
        public SqlServerRepository()
        {
        }

        protected override DbConnection GetDbConnection()
        {
            throw new NotImplementedException();
        }
    }
}
