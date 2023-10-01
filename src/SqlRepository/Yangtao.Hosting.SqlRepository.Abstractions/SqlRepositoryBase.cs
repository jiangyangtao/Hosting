using System.Data.Common;

namespace Yangtao.Hosting.SqlRepository.Abstractions
{
    public abstract class SqlRepositoryBase : ISqlRepository
    {

        public SqlRepositoryBase()
        {

        }

        protected abstract DbConnection GetDbConnection();
    }
}
