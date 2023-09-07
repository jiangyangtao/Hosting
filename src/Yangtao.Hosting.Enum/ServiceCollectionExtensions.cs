using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Enum
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEnum(this IServiceCollection services)
        {
            var assemblys = DependencyContext.Default.CompileLibraries.Where(a => a.Type == "project").Select(a => Assembly.Load(a.Name)).ToArray();
            if (assemblys.IsNullOrEmpty()) return services;

            var registerMethod = typeof(ServiceCollectionExtensions).GetMethod(nameof(RegisterEnumProvider), BindingFlags.Static | BindingFlags.NonPublic);
            foreach (var assembly in assemblys)
            {
                var enumTypes = assembly.GetTypes().Where(a => a.IsEnum).ToArray();
                if (enumTypes.IsNullOrEmpty()) continue;

                foreach (var enumType in enumTypes)
                {
                    var method = registerMethod.MakeGenericMethod(enumType);
                    registerMethod.GetGenericMethodDefinition();
                    registerMethod.Invoke(null, new object[] { services });
                }
            }

            return services;
        }

        private static void RegisterEnumProvider<TEnum>(IServiceCollection services) where TEnum : struct, System.Enum
        {
            services.AddSingleton<EnumProvider<TEnum>>();
        }
    }
}
