using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core.Builders
{
    public class ModelTypeBuilder
    {
        public ModelTypeBuilder()
        {
        }

        public static Type[] GetEntityModleTypes() => GetModleTypes(typeof(IEntity<>));

        public static Type[] GetViewModleTypes() => GetModleTypes(typeof(IView));

        private static Type[] GetModleTypes(Type modelType)
        {
            var assemblies = CurrnetAssemblies;

            var entityTypes = assemblies.SelectMany(assemblie => assemblie.GetTypes().Where(t => t.BaseType != null && t.BaseType == modelType)).ToArray();
            return entityTypes.Where(a => a.HasInterface<IProxyTargetAccessor>() == false).ToArray();  // 去除懒加载创建的实体代理
        }

        private static Assembly[] CurrnetAssemblies
        {
            get
            {
                if (_CurrnetAssemblies.IsNullOrEmpty())
                {
                    var compilationLibraries = DependencyContext.Default.CompileLibraries.Where(a => a.Serviceable == false && a.Type == "project").ToArray();
                    var assemblies = new List<Assembly>();
                    foreach (var compilationLibrary in compilationLibraries)
                    {
                        var assemblie = Assembly.Load(compilationLibrary.Name);
                        assemblies.Add(assemblie);
                    }

                    _CurrnetAssemblies = assemblies.ToArray();
                }

                return _CurrnetAssemblies;
            }
        }

        private static Assembly[] _CurrnetAssemblies { set; get; }
    }
}
