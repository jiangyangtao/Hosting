using Microsoft.EntityFrameworkCore;

namespace Yangtao.Hosting.Repository.Core
{
    internal class DefaultDbContext : DbContext
    {
        private readonly DbContextBuilder? contextBuilder;

        public DefaultDbContext(DbContextOptions options) : base(options)
        {
            contextBuilder = options.FindExtension<DbContextBuilder>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (contextBuilder != null && contextBuilder.OnConfiguring != null)
                contextBuilder.OnConfiguring(optionsBuilder);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            if (contextBuilder != null && contextBuilder.ConfigureConventions != null)
                contextBuilder.ConfigureConventions(configurationBuilder);

            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapEntities();
            modelBuilder.MapViews();

            if (contextBuilder != null && contextBuilder.OnModelCreating != null)
                contextBuilder.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
