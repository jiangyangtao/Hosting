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
            ServiceName = StaticDocument.DocumentHandler.GetServiceName();

            var httpActionAttribute = tableComponentType.ComponentType.GetCustomAttribute<HttpActionAttribute>();
            if (httpActionAttribute != null)
            {
                IsValidHttpAction = true;
                ActionApi = httpActionAttribute.ActionApi;
                HttpMethodType = httpActionAttribute.HttpMethodType;
                ApiVersion = httpActionAttribute.ApiVersion;
                ServiceName = StaticDocument.DocumentHandler.GetServiceName(httpActionAttribute.ServiceName);
            }

            Fields = tableComponentType.ComponentType.BuildFields();
            if (tableComponentType.ComponentAttribute.QueryForm != null)
            {
                IsPagination = IsPaginationType(tableComponentType.ComponentAttribute.QueryForm);

                QueryForm = new(tableComponentType.ComponentAttribute.QueryForm);
                if (QueryForm.Module.IsNullOrEmpty())
                {
                    QueryForm.Module = Module;
                    QueryForm.ModuleName = ModuleName;
                }
            }

            if (tableComponentType.ComponentAttribute.AddForm != null)
            {
                AddForm = new(tableComponentType.ComponentAttribute.AddForm);
                if (AddForm.Module.IsNullOrEmpty())
                {
                    AddForm.Module = Module;
                    AddForm.ModuleName = ModuleName;
                }
            }

            if (tableComponentType.ComponentAttribute.EditForm != null)
            {
                EditForm = new(tableComponentType.ComponentAttribute.EditForm);
                if (EditForm.Module.IsNullOrEmpty())
                {
                    EditForm.Module = Module;
                    EditForm.ModuleName = ModuleName;
                }
            }

            var properties = tableComponentType.ComponentType.GetProperties();
            var dataStatusProperty = properties.FirstOrDefault(a => a.GetCustomAttribute<DataStatusAttribute>() != null);
            if (dataStatusProperty != null) DataStatus = new DataStatus(dataStatusProperty);

            var deleteAction = tableComponentType.ComponentType.GetCustomAttribute<DeleteActionAttribute>();
            if (deleteAction != null) DeleteAction = new(deleteAction);

            var batchDeleteAction = tableComponentType.ComponentType.GetCustomAttribute<BatchDeleteActionAttribute>();
            if (batchDeleteAction != null) BatchDeleteAction = new(batchDeleteAction);

            var importAction = tableComponentType.ComponentType.GetCustomAttribute<ImportActionAttribute>();
            if (importAction != null) ImportAction = new(importAction, tableComponentType.ComponentType);

            var exportAction = tableComponentType.ComponentType.GetCustomAttribute<ExportActionAttribute>();
            if (exportAction != null) ExportAction = new(exportAction);

            Validate(tableComponentType.ComponentType);
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
        public HttpMethodType HttpMethodType { set; get; } = Enums.HttpMethodType.Get;

        public IEnumerable<FieldBase> Fields { set; get; }

        public bool IsPagination { set; get; } = false;

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsValidHttpAction { get; } = false;

        private void Validate(Type componentType)
        {
            base.ValidateModule(componentType);
            if (IsValidHttpAction == false) throw new InvalidOperationException($"{componentType.FullName} must specify set HttpAction or config HttpActionAttribute.");
        }
    }
}
