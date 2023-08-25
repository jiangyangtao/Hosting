using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Yangtao.Hosting.Repository.Core
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder AddEntities(this ModelBuilder modelBuilder)
        {
            var types = EntityTypeBuilder.GetEntityTypes();
            foreach (var type in types)
            {
                if (type.IsAbstract) continue;

                var r = modelBuilder.Entity(type);

                var tableAttribute = type.GetCustomAttribute<TableAttribute>();
                if (tableAttribute != null) r.ToTable(tableAttribute.Name);
            }

            return modelBuilder;
        }
    }
}
