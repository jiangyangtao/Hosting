using System.Data.Common;
using Yangtao.Hosting.SqlRepository.Core;

namespace Yangtao.Hosting.SqlRepository.MySql
{
    internal class MySqlRepository : SqlRepositoryBase
    {
        public MySqlRepository()
        {
        }

        protected override DbConnection GetDbConnection()
        {
            throw new NotImplementedException();
        }
    }
}
