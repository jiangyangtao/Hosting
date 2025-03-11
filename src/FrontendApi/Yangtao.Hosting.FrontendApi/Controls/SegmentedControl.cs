using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class SegmentedControl : ControlBase, IHttpAction
    {
        public SegmentedControl(SegmentedAttribute segmentedAttribute, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            Block = segmentedAttribute.Block;
        }

        public SegmentedControl(DictionaryOptionsAttribute dictionaryAttribute, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            if (dictionaryAttribute.ActionApi.NotNullAndEmpty() && dictionaryAttribute.ServiceName.NotNullAndEmpty())
            {
                ActionApi = dictionaryAttribute.ActionApi;
                HttpMethodType = dictionaryAttribute.HttpMethodType;
                ApiVersion = dictionaryAttribute.ApiVersion;
                ServiceName = documentHandler.GetServiceName(dictionaryAttribute.ServiceName);
            }

            if (documentHandler.DictionaryConfig != null && dictionaryAttribute.ActionApi.IsNullOrEmpty() && dictionaryAttribute.ServiceName.IsNullOrEmpty())
            {
                ActionApi = documentHandler.DictionaryConfig.ActionApi;
                ApiVersion = documentHandler.DictionaryConfig.ApiVersion;
                HttpMethodType = documentHandler.DictionaryConfig.HttpMethodType;
                ServiceName = documentHandler.DictionaryConfig.ServiceName;
            }
        }

        public override ControlType ControlType => ControlType.Segmented;

        public bool Block { set; get; } = false;

        public string? ActionApi { set; get; }

        public HttpMethodType? HttpMethodType { set; get; }

        public ApiVersion ApiVersion { set; get; }

        public string? ServiceName { set; get; }
    }
}
