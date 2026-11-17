using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Controls;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.FrontendApi.Fields;

namespace Yangtao.Hosting.FrontendApi.Components
{
    /// <summary>
    /// 独立操作基类
    /// </summary>
    internal abstract class IndependentActionBase : ActionBase
    {
        protected IndependentActionBase()
        {
            RequestParams = Array.Empty<ParamField>();
        }

        protected IndependentActionBase(IAction action) : this()
        {
            ActionApi = action.ActionApi;
            HttpMethodType = action.HttpMethodType;
            ApiVersion = action.ApiVersion;
            ServiceName = StaticDocument.DocumentHandler.GetServiceName(action.ServiceName);

            if (action.RequestData != null) RequestParams = action.RequestData.BuildParamFields();
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public abstract IndependentType IndependentType { get; }
    }

    /// <summary>
    /// 独立编辑
    /// </summary>
    internal class IndependentEdit : IndependentActionBase
    {
        public IndependentEdit(PropertyInfo property, IndependentEditAttribute independentEdit) : base(independentEdit)
        {
            Control = property.BuildControl();
        }

        public ControlBase Control { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.Edit;
    }

    /// <summary>
    /// 存在验证
    /// </summary>
    internal class ExistValidation : IndependentActionBase
    {
        public ExistValidation(ExistValidationAttribute existValidation) : base(existValidation)
        {
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.ExistValidate;
    }

    /// <summary>
    /// 删除操作
    /// </summary>
    internal class DeleteAction : IndependentActionBase
    {
        public DeleteAction(DeleteActionAttribute actionAttribute) : base(actionAttribute)
        {
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.Delete;
    }

    internal class BatchDeleteAction : IndependentActionBase
    {
        public BatchDeleteAction(BatchDeleteActionAttribute actionAttribute) : base(actionAttribute)
        {
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.BatchDelete;
    }

    internal class ImportAction : IndependentActionBase
    {
        public ImportAction(ImportActionAttribute actionAttribute) : base(actionAttribute)
        {
            if (actionAttribute.HasImportTemplate)
            {
                TemplateAction = new ImportTemplateAction(actionAttribute.ImportTemplateController, actionAttribute.ImportTemplateMethod);
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.Import;

        public ImportTemplateAction? TemplateAction { get; }

        [JsonIgnore]
        public override Type? RequestData { protected set; get; } = null;

        public class ImportTemplateAction : ActionBase
        {
            internal ImportTemplateAction(Type controller, string methodName)
            {
                var method = controller.GetMethod(methodName) ?? throw new ArgumentException($"${methodName} does not exist in the {controller.Name}");
                var action = method.GetHttpAction();

                ActionApi = action.ActionApi;
                HttpMethodType = action.HttpMethodType;
                ActionApi = action.ActionApi;
                ServiceName = StaticDocument.DocumentHandler.GetServiceName(action.ServiceName);
            }

            [JsonIgnore]
            public override Type? RequestData { protected set; get; } = null;
        }
    }

    internal class ExportAction : IndependentActionBase
    {
        public ExportAction(ExportActionAttribute actionAttribute) : base(actionAttribute)
        {

        }

        public override IndependentType IndependentType => IndependentType.Export;

        [JsonIgnore]
        public override Type? RequestData { protected set; get; } = null;
    }
}
