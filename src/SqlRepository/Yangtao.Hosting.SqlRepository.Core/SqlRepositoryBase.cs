using System.Data.Common;
using Yangtao.Hosting.SqlRepository.Abstractions;

namespace Yangtao.Hosting.SqlRepository.Core
{
    public abstract class SqlRepositoryBase : ISqlRepository
    {

        public SqlRepositoryBase()
        {

        }

        protected abstract DbConnection GetDbConnection();
    }
}
