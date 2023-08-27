using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.Repository.Core
{
    public class DbContextBuilder : IDbContextOptionsExtension
    {
        public DbContextBuilder()
        {
        }

        private DbContextOptionsExtensionInfo _info;

        public Action<ModelBuilder> OnModelCreating;

        public Action<DbContextOptionsBuilder> OnConfiguring;

        public Action<ModelConfigurationBuilder> ConfigureConventions;

        public DbContextOptionsExtensionInfo Info => _info ??= new DefaultDbContextOptionsExtensionInfo(this);

        public void ApplyServices(IServiceCollection services)
        {
        }

        public void Validate(IDbContextOptions options)
        {
        }

    }

    internal class DefaultDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
    {
        public DefaultDbContextOptionsExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
        }

        public override bool IsDatabaseProvider => true;

        public override string LogFragment => string.Empty;

        public override int GetServiceProviderHashCode() => 0;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
        }

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => other is DefaultDbContextOptionsExtensionInfo;
    }
}
