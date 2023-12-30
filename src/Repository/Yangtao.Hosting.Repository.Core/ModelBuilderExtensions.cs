using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Core.Attribute;
using Yangtao.Hosting.Repository.Core.Builders;

namespace Yangtao.Hosting.Repository.Core
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder MapEntities(this ModelBuilder modelBuilder)
        {
            var service = ModelTypeBuilder.GetEntityModelService();
            var types = service.GetEntityTypes();
            foreach (var type in types)
            {
                if (type.IsAbstract) continue;

                var r = modelBuilder.Entity(type);

                var tableAttribute = type.GetCustomAttribute<TableAttribute>();
                if (tableAttribute != null)
                {
                    if (tableAttribute.Schema.IsNullOrEmpty())
                        r.ToTable(tableAttribute.Name);

                    if (tableAttribute.Schema.NotNullAndEmpty())
                        r.ToTable(tableAttribute.Name, tableAttribute.Schema);
                }
            }

            return modelBuilder;
        }

        public static ModelBuilder MapViews(this ModelBuilder modelBuilder)
        {
            var types = ModelTypeBuilder.GetViewModelTypes();
            foreach (var type in types)
            {
                if (type.IsAbstract) continue;

                var r = modelBuilder.Entity(type);

                var viewAttribute = type.GetCustomAttribute<ViewAttribute>();
                if (viewAttribute == null) r.ToView(type.Name);

                if (viewAttribute != null)
                {
                    r.ToView(viewAttribute.Name);

                    if (viewAttribute.Schema.IsNullOrEmpty())
                        r.ToView(viewAttribute.Name);

                    if (viewAttribute.Schema.NotNullAndEmpty())
                        r.ToView(viewAttribute.Name, viewAttribute.Schema);
                }
            }

            return modelBuilder;
        }
    }
}
