using Yangtao.Hosting.FrontendApi.Components;

namespace Yangtao.Hosting.FrontendApi
{
    internal class FrontendModule
    {
        private readonly IDictionary<string, FormComponent> Forms;

        private readonly IDictionary<string, QueryFormComponent> QueryForms;

        private readonly IDictionary<string, TableComponent> Tables;

        public FrontendModule(string module)
        {
            Tables = new Dictionary<string, TableComponent>();
            Forms = new Dictionary<string, FormComponent>();
            QueryForms = new Dictionary<string, QueryFormComponent>();
            Module = module;
        }

        public string Module { get; }

        public void AddTable(TableComponent tableComponent)
        {
            if (tableComponent == null) return;
            if (tableComponent.IsValidName == false) return;

            var exist = Tables.ContainsKey(tableComponent.ComponentTypeFullName);
            if (exist) throw new InvalidOperationException($"{Module} has registered {tableComponent.ComponentTypeFullName}.");

            var existComponent = Tables.Values.Any(a => a.ComponentName == tableComponent.ComponentName);
            if (existComponent) throw new InvalidOperationException($"{Module} has registered {tableComponent.ComponentName}.");

            Tables.Add(tableComponent.ComponentTypeFullName, tableComponent);

            // 同时在模块上注册，增加定位入口
            AddForm(tableComponent.AddForm);
            AddForm(tableComponent.EditForm);
            AddQueryForm(tableComponent.QueryForm);
        }

        public void AddForm(FormComponent? component)
        {
            if (component == null) return;
            if (component.IsValidName == false) return;

            var exist = Forms.ContainsKey(component.ComponentTypeFullName);
            if (exist) throw new InvalidOperationException($"{Module} has registered {component.ComponentTypeFullName}.");

            var existComponent = Forms.Values.Any(a => a.ComponentName == component.ComponentName);
            if (existComponent) throw new InvalidOperationException($"{Module} has registered {component.ComponentName}.");

            Forms.Add(component.ComponentTypeFullName, component);
        }

        public void AddQueryForm(QueryFormComponent? queryForm)
        {
            if (queryForm == null) return;
            if (queryForm.IsValidName == false) return;

            var exist = QueryForms.ContainsKey(queryForm.ComponentTypeFullName);
            if (exist) throw new InvalidOperationException($"{Module} has registered {queryForm.ComponentTypeFullName}.");

            var existComponent = QueryForms.Values.Any(a => a.ComponentName == queryForm.ComponentName);
            if (existComponent) throw new InvalidOperationException($"{Module} has registered {queryForm.ComponentName}.");

            QueryForms.Add(queryForm.ComponentTypeFullName, queryForm);
        }

        public IEnumerable<TableComponent> TableComponents => Tables.Values;

        public IEnumerable<FormComponent> FormComponents => Forms.Values;

        public IEnumerable<QueryFormComponent> QueryFormComponents => QueryForms.Values;
    }
}
