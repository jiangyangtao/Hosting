using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Repository.Core.Builders
{
    internal class EntityModelRegisterHandler
    {
        public readonly StringEntityType StringEntityType;

        public readonly GuidEntityType GuidEntityType;

        public readonly IntegerEntityType IntegerEntityType;

        public readonly BigIntegerEntityType BigIntegerEntityType;

        public EntityModelRegisterHandler()
        {
            StringEntityType = new();
            GuidEntityType = new();
            IntegerEntityType = new();
            BigIntegerEntityType = new();
        }

        public void Build(Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                if (types.IsNullOrEmpty()) continue;

                foreach (var type in types)
                {
                    if (type.FullName.IsNullOrEmpty()) continue;

                    var interfaces = type.GetInterfaces();
                    if (interfaces.IsNullOrEmpty()) continue;

                    StringEntityType.Add(type);
                    GuidEntityType.Add(type);
                    IntegerEntityType.Add(type);
                    BigIntegerEntityType.Add(type);
                }
            }
        }

        public Type[] GetEntityTypes()
        {
            var types = new List<Type>();
            if (StringEntityType.EntityTypes.NotNullAndEmpty()) types.AddRange(StringEntityType.EntityTypes);
            if (GuidEntityType.EntityTypes.NotNullAndEmpty()) types.AddRange(GuidEntityType.EntityTypes);
            if (IntegerEntityType.EntityTypes.NotNullAndEmpty()) types.AddRange(IntegerEntityType.EntityTypes);
            if (BigIntegerEntityType.EntityTypes.NotNullAndEmpty()) types.AddRange(BigIntegerEntityType.EntityTypes);

            return types.ToArray();
        }

        public void Register(IServiceCollection services)
        {
            StringEntityType.Register(services);
            GuidEntityType.Register(services);
            IntegerEntityType.Register(services);
            BigIntegerEntityType.Register(services);
        }
    }
}
