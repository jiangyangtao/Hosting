using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Controls;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Components
{
    /// <summary>
    /// 独立操作基类
    /// </summary>
    internal abstract class IndependentActionBase : ActionBase
    {
        protected IndependentActionBase(IAction action) : base(action)
        {

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

        [JsonConverter(typeof(StringEnumConverter))]
        public override HttpMethodType HttpMethodType { protected set; get; } = HttpMethodType.Get;
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

        [JsonConverter(typeof(StringEnumConverter))]
        public override HttpMethodType HttpMethodType { protected set; get; } = HttpMethodType.Delete;
    }

    /// <summary>
    /// 批量删除操作
    /// </summary>
    internal class BatchDeleteAction : IndependentActionBase
    {
        public BatchDeleteAction(BatchDeleteActionAttribute actionAttribute) : base(actionAttribute)
        {
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.BatchDelete;

        [JsonConverter(typeof(StringEnumConverter))]
        public override HttpMethodType HttpMethodType { protected set; get; } = HttpMethodType.Delete;
    }

    internal class ImportAction : IndependentActionBase
    {
        public ImportAction(ImportActionAttribute actionAttribute, Type componentType) : base(actionAttribute)
        {
            var importTemplateActionAttr = componentType.GetCustomAttribute<ImportTemplateActionAttribute>(false);
            if (importTemplateActionAttr != null) TemplateAction = new(importTemplateActionAttr);
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.Import;

        [JsonConverter(typeof(StringEnumConverter))]
        public override HttpMethodType HttpMethodType { protected set; get; } = HttpMethodType.Post;

        public ImportTemplateAction? TemplateAction { get; }

        [JsonIgnore]
        public override Type? RequestData { protected set; get; } = null;

        public class ImportTemplateAction : ActionBase
        {
            internal ImportTemplateAction(ImportTemplateActionAttribute action) : base(action)
            {

            }

            [JsonIgnore]
            public override Type? RequestData { protected set; get; } = null;

            [JsonConverter(typeof(StringEnumConverter))]
            public override HttpMethodType HttpMethodType { protected set; get; } = HttpMethodType.Get;
        }
    }

    internal class ExportAction : IndependentActionBase
    {
        public ExportAction(ExportActionAttribute actionAttribute) : base(actionAttribute)
        {

        }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.Export;

        [JsonConverter(typeof(StringEnumConverter))]
        public override HttpMethodType HttpMethodType { protected set; get; } = HttpMethodType.Get;

        [JsonIgnore]
        public override Type? RequestData { protected set; get; } = null;
    }
}
