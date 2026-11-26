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
    internal class TableComponent : ComponentBase, IHttpAction
    {
        public TableComponent(ComponentTypeInfo<TableComponentAttribute> tableComponentType) : base(tableComponentType.ComponentType)
        {
            var httpActionAttr = tableComponentType.ComponentType.GetCustomAttribute<HttpActionAttribute>(false) ?? throw new InvalidOperationException($"{tableComponentType.ComponentType} must config HttpActionAttribute.");
            SetHttpAction(httpActionAttr);
            if (ActionApi.IsNullOrEmpty()) throw new InvalidOperationException($"{tableComponentType.ComponentType} the HttpActionAttribute must specify set HttpAction.");

            if (tableComponentType.ComponentAttribute.AddForm != null &&
                tableComponentType.ComponentAttribute.EditForm != null &&
                tableComponentType.ComponentAttribute.AddForm.FullName == tableComponentType.ComponentAttribute.EditForm.FullName)
            {
                throw new InvalidOperationException($"AddForm:[{tableComponentType.ComponentAttribute.AddForm.FullName}] and EditForm:[{tableComponentType.ComponentAttribute.EditForm.FullName}] are prohibited from using the same class.");
            }

            Fields = tableComponentType.ComponentType.BuildFields();
            if (tableComponentType.ComponentAttribute.QueryForm != null)
            {
                IsPagination = IsPaginationType(tableComponentType.ComponentAttribute.QueryForm);

                QueryForm = new(tableComponentType.ComponentAttribute.QueryForm)
                {
                    Module = Module,
                    ModuleName = ModuleName,
                };
            }

            if (tableComponentType.ComponentAttribute.AddForm != null)
            {
                AddForm = new(tableComponentType.ComponentAttribute.AddForm)
                {
                    Module = Module,
                    ModuleName = ModuleName
                };
            }

            if (tableComponentType.ComponentAttribute.EditForm != null)
            {
                EditForm = new(tableComponentType.ComponentAttribute.EditForm)
                {
                    Module = Module,
                    ModuleName = ModuleName
                };
            }

            var properties = tableComponentType.ComponentType.GetProperties();
            var dataStatusProperty = properties.FirstOrDefault(a => a.GetCustomAttribute<DataStatusAttribute>(false) != null);
            if (dataStatusProperty != null) DataStatus = new DataStatus(dataStatusProperty);

            var deleteAction = tableComponentType.ComponentType.GetCustomAttribute<DeleteActionAttribute>(false);
            if (deleteAction != null) DeleteAction = new(deleteAction);

            var batchDeleteAction = tableComponentType.ComponentType.GetCustomAttribute<BatchDeleteActionAttribute>(false);
            if (batchDeleteAction != null) BatchDeleteAction = new(batchDeleteAction);

            var importAction = tableComponentType.ComponentType.GetCustomAttribute<ImportActionAttribute>(false);
            if (importAction != null) ImportAction = new(importAction, tableComponentType.ComponentType);

            var exportAction = tableComponentType.ComponentType.GetCustomAttribute<ExportActionAttribute>(false);
            if (exportAction != null) ExportAction = new(exportAction);

            ValidateModule(tableComponentType.ComponentType);
        }

        private void SetHttpAction(HttpActionAttributeBase httpAction)
        {
            ActionApi = httpAction.ActionApi;
            HttpMethodType = httpAction.HttpMethodType;
            ApiVersion = httpAction.ApiVersion;
            ServiceName = StaticDocument.DocumentHandler.GetServiceName(httpAction.ServiceName);
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
        public HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Get;

        public IEnumerable<FieldBase> Fields { set; get; }

        public bool IsPagination { set; get; } = false;

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }
}
