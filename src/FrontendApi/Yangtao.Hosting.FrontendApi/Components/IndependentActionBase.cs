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
    internal abstract class IndependentActionBase : IAction
    {
        private IndependentActionBase()
        {
            RequestParams = Array.Empty<ParamField>();
        }

        protected IndependentActionBase(IAction action, XmlDocumentHandler xmlHandler) : this()
        {
            ActionApi = action.ActionApi;
            HttpActionType = action.HttpActionType;
            HttpVersion = action.HttpVersion;
            ServiceName = action.ServiceName;

            if (action.RequestData != null) RequestParams = action.RequestData.BuildParamFields(xmlHandler);
        }

        [JsonIgnore]
        public Type? RequestData { get; }

        public IEnumerable<ParamField> RequestParams { get; }

        public string? ActionApi { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpActionType { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public abstract IndependentType IndependentType { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpVersion HttpVersion { set; get; } = HttpVersion.v1;

        public string? ServiceName { set; get; }
    }

    internal class IndependentEdit : IndependentActionBase
    {
        public IndependentEdit(PropertyInfo property, IndependentEditAttribute independentEdit, XmlDocumentHandler xmlHandler) : base(independentEdit, xmlHandler)
        {
            Control = property.BuildControl(xmlHandler);
        }

        public ControlBase Control { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.Edit;
    }

    internal class ExistValidation : IndependentActionBase
    {
        public ExistValidation(ExistValidationAttribute existValidation, XmlDocumentHandler xmlHandler) : base(existValidation, xmlHandler)
        {
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.ExistValidate;
    }

    internal class DeleteAction : IndependentActionBase
    {
        public DeleteAction(DeleteActionAttribute actionAttribute, XmlDocumentHandler xmlHandler) : base(actionAttribute, xmlHandler)
        {
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public override IndependentType IndependentType => IndependentType.Delete;
    }
}
