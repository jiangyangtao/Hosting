using Microsoft.Extensions.Logging;
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
        private readonly IDictionary<string, FrontendModule> _frontendModules;
        private readonly IEnumerable<Assembly> _assemblies;
        private readonly ICollection<ComponentTypeInfo<TableComponentAttribute>> _tableComponentTypes;
        private readonly ICollection<ComponentTypeInfo<FormAttribute>> _formComponentTypes;
        private readonly ICollection<ComponentTypeInfo<QueryFormAttribute>> _queryFormComponentTypes;

        private FrontendModules()
        {
            _tableComponentTypes = new Collection<ComponentTypeInfo<TableComponentAttribute>>();
            _formComponentTypes = new Collection<ComponentTypeInfo<FormAttribute>>();
            _queryFormComponentTypes = new Collection<ComponentTypeInfo<QueryFormAttribute>>();

            _assemblies = Array.Empty<Assembly>();
            _frontendModules = new Dictionary<string, FrontendModule>();
        }

        public FrontendModules(IEnumerable<Assembly> assemblies, FrontendApiConfigurationOptions options, ILogger logger) : this()
        {
            _assemblies = assemblies;
            var documentHandler = new DocumentHandler(options);
            StaticDocument.SetDocumentHandler(documentHandler, logger);

            InitAttributeType();
            BuildTableComponent();
            BuildFormComponent();
            BuildQueryFormComponent();
        }

        private void InitAttributeType()
        {
            foreach (var assembly in _assemblies)
            {
                Type[] assemblyTypes;
                try { assemblyTypes = assembly.GetTypes(); }
                catch (ReflectionTypeLoadException ex) { assemblyTypes = ex.Types!.Where(t => t != null).ToArray()!; }

                foreach (var assemblyType in assemblyTypes)
                {
                    var tableComponentAttribute = assemblyType.GetCustomAttribute<TableComponentAttribute>(false);
                    if (tableComponentAttribute != null) _tableComponentTypes.Add(new ComponentTypeInfo<TableComponentAttribute>(assemblyType, tableComponentAttribute));

                    var formComponentAttribute = assemblyType.GetCustomAttribute<FormAttribute>(false);
                    if (formComponentAttribute != null) _formComponentTypes.Add(new ComponentTypeInfo<FormAttribute>(assemblyType, formComponentAttribute));

                    var queryFormComponentAttribute = assemblyType.GetCustomAttribute<QueryFormAttribute>(false);
                    if (queryFormComponentAttribute != null) _queryFormComponentTypes.Add(new ComponentTypeInfo<QueryFormAttribute>(assemblyType, queryFormComponentAttribute));
                }
            }
        }

        private void BuildTableComponent()
        {
            if (_tableComponentTypes.IsNullOrEmpty()) return;

            foreach (var tableComponentType in _tableComponentTypes)
            {
                var tableComponent = new TableComponent(tableComponentType);
                AddTable(tableComponent);
            }
        }

        private void BuildFormComponent()
        {
            if (_formComponentTypes.IsNullOrEmpty()) return;

            foreach (var formComponentType in _formComponentTypes)
            {
                var form = new FormComponent(formComponentType.ComponentType, formComponentType.ComponentAttribute);
                form.ValidateModule(formComponentType.ComponentType);
                AddForm(form);
            }
        }

        private void BuildQueryFormComponent()
        {
            if (_queryFormComponentTypes.IsNullOrEmpty()) return;

            foreach (var queryFormComponentType in _queryFormComponentTypes)
            {
                var queryForm = new QueryFormComponent(queryFormComponentType.ComponentType);
                queryForm.ValidateModule(queryFormComponentType.ComponentType);
                AddQueryForm(queryForm);
            }
        }

        private void AddTable(TableComponent tableComponent)
        {
            var module = GetModule(tableComponent.Module);
            module.AddTable(tableComponent);
        }

        private void AddForm(FormComponent formComponent)
        {
            var module = GetModule(formComponent.Module);
            module.AddForm(formComponent);
        }

        private void AddQueryForm(QueryFormComponent queryFormComponent)
        {
            var module = GetModule(queryFormComponent.Module);
            module.AddQueryForm(queryFormComponent);
        }

        private FrontendModule GetModule(string module)
        {
            var exist = _frontendModules.ContainsKey(module);
            if (exist) return _frontendModules[module];

            var r = new FrontendModule(module);
            _frontendModules.Add(module, r);

            return r;
        }

        public IEnumerator<FrontendModule> GetEnumerator() => _frontendModules.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string ToJson() => JsonConvert.SerializeObject(_frontendModules.Values);
    }
}
