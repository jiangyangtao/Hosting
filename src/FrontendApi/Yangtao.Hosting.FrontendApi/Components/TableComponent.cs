using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;

namespace Yangtao.Hosting.FrontendApi.Components
{
    internal class TableComponent : TableComponentBase
    {
        public TableComponent(Type tableComponentType, XmlDocumentHandler xmlHandler) : base(tableComponentType, xmlHandler)
        {
            var tableComponentAttribute = tableComponentType.GetCustomAttribute<TableComponentAttribute>() ??
                throw new ArgumentException($"{nameof(tableComponentType)} not exist {nameof(TableComponentAttribute)}");

            if (tableComponentAttribute.QueryForm != null)
            {
                QueryForm = new(tableComponentAttribute.QueryForm, xmlHandler);
                if (QueryForm.Module.IsNullOrEmpty())
                {
                    QueryForm.Module = Module;
                    QueryForm.ModuleName = ModuleName;
                }
            }

            if (tableComponentAttribute.AddForm != null)
            {
                AddForm = new(tableComponentAttribute.AddForm, xmlHandler);
                if (AddForm.Module.IsNullOrEmpty())
                {
                    AddForm.Module = Module;
                    AddForm.ModuleName = ModuleName;
                }
            }

            if (tableComponentAttribute.EditForm != null)
            {
                EditForm = new(tableComponentAttribute.EditForm, xmlHandler);
                if (EditForm.Module.IsNullOrEmpty())
                {
                    EditForm.Module = Module;
                    EditForm.ModuleName = ModuleName;
                }
            }

            var deleteActionAttribute = tableComponentType.GetCustomAttribute<DeleteActionAttribute>();
            if (deleteActionAttribute != null) DeleteAction = new(deleteActionAttribute, xmlHandler);

            var properties = tableComponentType.GetProperties();
            var dataStatusProperty = properties.FirstOrDefault(a => a.PropertyType.GetCustomAttribute<DataStatusAttribute>() != null);
            if (dataStatusProperty != null) DataStatus = new DataStatus(dataStatusProperty.PropertyType, xmlHandler);

            var deleteAction = tableComponentType.GetCustomAttribute<DeleteActionAttribute>();
            if (deleteAction != null) DeleteAction = new(deleteAction, xmlHandler);
        }

        public QueryFormComponent? QueryForm { set; get; }

        public FormComponent? AddForm { set; get; }

        public FormComponent? EditForm { set; get; }

        public IDataStatus? DataStatus { set; get; }

        public DeleteAction? DeleteAction { set; get; }
    }
}
