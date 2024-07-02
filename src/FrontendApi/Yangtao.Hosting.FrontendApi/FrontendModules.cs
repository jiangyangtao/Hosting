using Newtonsoft.Json;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Components;

namespace Yangtao.Hosting.FrontendApi
{
    internal class FrontendModules : IEnumerable<FrontendModule>
    {
        private readonly IDictionary<string, FrontendModule> frontendModules;
        private readonly IEnumerable<Assembly> Assemblies;

        public FrontendModules(IEnumerable<Assembly> assemblies, string       serviceName)
        {
            frontendModules = new Dictionary<string, FrontendModule>();
            Assemblies = assemblies;

            var documentHandler = new DocumentHandler(serviceName);
            BuildTableComponent(documentHandler);
            BuildFormComponent(documentHandler);
            BuildQueryFormComponent(documentHandler);
        }

        private void BuildTableComponent(DocumentHandler documentHandler)
        {
            var tableComponentTypes = GetTypes(typeof(TableComponentAttribute));
            if (tableComponentTypes.IsNullOrEmpty()) return;

            foreach (var tableComponentType in tableComponentTypes)
            {
                var tableComponent = new TableComponent(tableComponentType, documentHandler);
                if (tableComponent.Module.IsNullOrEmpty()) continue;

                var module = GetModule(tableComponent.Module);
                module.AddTable(tableComponent);
            }
        }

        private void BuildFormComponent(DocumentHandler documentHandler)
        {
            var formComponentTypes = GetTypes(typeof(FormAttribute));
            if (formComponentTypes.IsNullOrEmpty()) return;

            foreach (var formComponentType in formComponentTypes)
            {
                var form = new FormComponent(formComponentType, documentHandler);
                if (form.IsEmptyModule) continue;

                var module = GetModule(form.Module);
                module.AddForm(form);
            }
        }

        private void BuildQueryFormComponent(DocumentHandler documentHandler)
        {
            var queryFormComponentTypes = GetTypes(typeof(QueryFormAttribute));
            if (queryFormComponentTypes.IsNullOrEmpty()) return;

            foreach (var queryFormComponentType in queryFormComponentTypes)
            {
                var queryForm = new QueryFormComponent(queryFormComponentType, documentHandler);
                if (queryForm.IsEmptyModule) continue;

                var module = GetModule(queryForm.Module);
                module.AddQueryForm(queryForm);
            }
        }

        private IEnumerable<Type> GetTypes(Type targetType)
        {
            var types = new Collection<Type>();
            foreach (var assembly in Assemblies)
            {
                var assemblyTypes = assembly.GetTypes();
                foreach (var assemblyType in assemblyTypes)
                {
                    var r = assemblyType.GetCustomAttribute(targetType, false);
                    if (r == null) continue;

                    types.Add(assemblyType);
                }
            }

            return types;
        }

        private bool ExistModule(string module) => frontendModules.ContainsKey(module);

        private FrontendModule GetModule(string module)
        {
            var exist = ExistModule(module);
            if (exist) return frontendModules[module];

            var r = new FrontendModule(module);
            frontendModules.Add(module, r);

            return r;
        }

        public IEnumerator<FrontendModule> GetEnumerator() => frontendModules.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string ToJson() => JsonConvert.SerializeObject(frontendModules.Values);
    }
}
