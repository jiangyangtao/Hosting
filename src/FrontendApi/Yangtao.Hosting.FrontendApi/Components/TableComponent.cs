using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.FrontendApi.Fields;

namespace Yangtao.Hosting.FrontendApi.Components
{
    internal class TableComponent : ComponentBase
    {
        public TableComponent(Type tableComponentType, DocumentHandler documentHandler) : base(tableComponentType, documentHandler)
        {
            var httpActionAttribute = tableComponentType.GetCustomAttribute<HttpActionAttribute>();
            if (httpActionAttribute != null)
            {
                ActionApi = httpActionAttribute.ActionApi;
                HttpActionType = httpActionAttribute.HttpActionType;
                HttpVersion = httpActionAttribute.HttpVersion;
                ServiceName = documentHandler.GetServiceName(httpActionAttribute.ServiceName);
            }

            Fields = tableComponentType.BuildFields(documentHandler);

            var tableComponentAttribute = tableComponentType.GetCustomAttribute<TableComponentAttribute>() ??
                throw new ArgumentException($"{nameof(tableComponentType)} not exist {nameof(TableComponentAttribute)}");

            if (tableComponentAttribute.QueryForm != null)
            {
                IsPagination = IsPaginationType(tableComponentAttribute.QueryForm);

                QueryForm = new(tableComponentAttribute.QueryForm, documentHandler);
                if (QueryForm.Module.IsNullOrEmpty())
                {
                    QueryForm.Module = Module;
                    QueryForm.ModuleName = ModuleName;
                }
            }

            if (tableComponentAttribute.AddForm != null)
            {
                AddForm = new(tableComponentAttribute.AddForm, documentHandler);
                if (AddForm.Module.IsNullOrEmpty())
                {
                    AddForm.Module = Module;
                    AddForm.ModuleName = ModuleName;
                }
            }

            if (tableComponentAttribute.EditForm != null)
            {
                EditForm = new(tableComponentAttribute.EditForm, documentHandler);
                if (EditForm.Module.IsNullOrEmpty())
                {
                    EditForm.Module = Module;
                    EditForm.ModuleName = ModuleName;
                }
            }

            var properties = tableComponentType.GetProperties();
            var dataStatusProperty = properties.FirstOrDefault(a => a.GetCustomAttribute<DataStatusAttribute>() != null);
            if (dataStatusProperty != null) DataStatus = new DataStatus(dataStatusProperty, documentHandler);

            var deleteAction = tableComponentType.GetCustomAttribute<DeleteActionAttribute>();
            if (deleteAction != null) DeleteAction = new(deleteAction, documentHandler);
        }

        private static bool IsPaginationType(Type? type)
        {
            if (type == null) return false;
            if (type == StaticKeys.PaginationType) return true;
            if (type.BaseType == null) return false;

            return IsPaginationType(type.BaseType);
        }

        public QueryFormComponent? QueryForm { set; get; }

        public FormComponent? AddForm { set; get; }

        public FormComponent? EditForm { set; get; }

        public IDataStatus? DataStatus { set; get; }

        public DeleteAction? DeleteAction { set; get; }


        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpActionType { set; get; } = HttpMethodType.Get;

        public IEnumerable<FieldBase> Fields { set; get; }

        public bool IsPagination { set; get; } = false;

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpVersion HttpVersion { set; get; } = HttpVersion.v1;

        public string? ServiceName { set; get; }
    }
}
