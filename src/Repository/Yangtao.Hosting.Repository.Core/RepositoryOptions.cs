using Microsoft.EntityFrameworkCore;

namespace Yangtao.Hosting.Repository.Core
{
    internal class RepositoryOptions
    {
        public DbContext DbContext { set; get; }
    }
}
