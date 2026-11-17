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
    /// <summary>
    /// 表格组件
    /// </summary>
    internal class TableComponent : ComponentBase
    {
        public TableComponent(Type tableComponentType) : base(tableComponentType)
        {
            var httpActionAttribute = tableComponentType.GetCustomAttribute<HttpActionAttribute>();
            if (httpActionAttribute != null)
            {
                ActionApi = httpActionAttribute.ActionApi;
                HttpMethodType = httpActionAttribute.HttpMethodType;
                ApiVersion = httpActionAttribute.ApiVersion;
                ServiceName = StaticDocument.DocumentHandler.GetServiceName(httpActionAttribute.ServiceName);
            }

            Fields = tableComponentType.BuildFields();

            var tableComponentAttribute = tableComponentType.GetCustomAttribute<TableComponentAttribute>() ??
                throw new ArgumentException($"{nameof(tableComponentType)} not exist {nameof(TableComponentAttribute)}");

            if (tableComponentAttribute.QueryForm != null)
            {
                IsPagination = IsPaginationType(tableComponentAttribute.QueryForm);

                QueryForm = new(tableComponentAttribute.QueryForm);
                if (QueryForm.Module.IsNullOrEmpty())
                {
                    QueryForm.Module = Module;
                    QueryForm.ModuleName = ModuleName;
                }
            }

            if (tableComponentAttribute.AddForm != null)
            {
                AddForm = new(tableComponentAttribute.AddForm);
                if (AddForm.Module.IsNullOrEmpty())
                {
                    AddForm.Module = Module;
                    AddForm.ModuleName = ModuleName;
                }
            }

            if (tableComponentAttribute.EditForm != null)
            {
                EditForm = new(tableComponentAttribute.EditForm);
                if (EditForm.Module.IsNullOrEmpty())
                {
                    EditForm.Module = Module;
                    EditForm.ModuleName = ModuleName;
                }
            }

            var properties = tableComponentType.GetProperties();
            var dataStatusProperty = properties.FirstOrDefault(a => a.GetCustomAttribute<DataStatusAttribute>() != null);
            if (dataStatusProperty != null) DataStatus = new DataStatus(dataStatusProperty);

            var deleteAction = tableComponentType.GetCustomAttribute<DeleteActionAttribute>();
            if (deleteAction != null) DeleteAction = new(deleteAction);

            var batchDeleteAction = tableComponentType.GetCustomAttribute<BatchDeleteActionAttribute>();
            if (batchDeleteAction != null) BatchDeleteAction = new(batchDeleteAction);

            var importAction = tableComponentType.GetCustomAttribute<ImportActionAttribute>();
            if (importAction != null) ImportAction = new(importAction);

            var exportAction = tableComponentType.GetCustomAttribute<ExportActionAttribute>();
            if (exportAction != null) ExportAction = new(exportAction);
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

        public BatchDeleteAction? BatchDeleteAction { set; get; }

        public ImportAction? ImportAction { set; get; }

        public ExportAction? ExportAction { set; get; }


        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpMethodType { set; get; } = Enums.HttpMethodType.Get;

        public IEnumerable<FieldBase> Fields { set; get; }

        public bool IsPagination { set; get; } = false;

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }
}
