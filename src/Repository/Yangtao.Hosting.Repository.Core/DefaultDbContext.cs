using Microsoft.EntityFrameworkCore;

namespace Yangtao.Hosting.Repository.Core
{
    internal class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapEntities();
            modelBuilder.MapViews();
            base.OnModelCreating(modelBuilder);
        }
    }
}
