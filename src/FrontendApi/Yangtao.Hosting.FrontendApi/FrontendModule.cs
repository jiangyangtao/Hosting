using Yangtao.Hosting.Extensions;
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
            if (tableComponent.ComponentName.IsNullOrEmpty()) return;

            var exist = Tables.ContainsKey(tableComponent.ComponentName);
            if (exist) return;

            Tables.TryAdd(tableComponent.ComponentName, tableComponent);
            AddForm(tableComponent.AddForm);
            AddForm(tableComponent.EditForm);
            AddQueryForm(tableComponent.QueryForm);
        }

        public void AddForm(FormComponent? component)
        {
            if (component == null) return;
            if (component.ComponentName.IsNullOrEmpty()) return;

            var exist = Forms.ContainsKey(component.ComponentName);
            if (exist) return;

            Forms.TryAdd(component.ComponentName, component);
        }

        public void AddQueryForm(QueryFormComponent? queryForm)
        {
            if (queryForm == null) return;
            if (queryForm.ComponentName.IsNullOrEmpty()) return;

            var exist = QueryForms.ContainsKey(queryForm.ComponentName);
            if (exist) return;

            QueryForms.TryAdd(queryForm.ComponentName, queryForm);
        }

        public IEnumerable<TableComponent> TableComponents => Tables.Values;

        public IEnumerable<FormComponent> FormComponents => Forms.Values;

        public IEnumerable<QueryFormComponent> QueryFormComponents => QueryForms.Values;
    }
}
