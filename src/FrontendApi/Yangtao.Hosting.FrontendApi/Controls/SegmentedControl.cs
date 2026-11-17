using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class SegmentedControl : ControlBase, IHttpAction
    {
        public SegmentedControl(SegmentedAttribute segmentedAttribute, PropertyInfo property) : base(property)
        {
            Block = segmentedAttribute.Block;
        }

        public SegmentedControl(DictionaryOptionsAttribute dictionaryAttribute, PropertyInfo property) : base(property)
        {
            if (dictionaryAttribute.ActionApi.NotNullAndEmpty() && dictionaryAttribute.ServiceName.NotNullAndEmpty())
            {
                ActionApi = dictionaryAttribute.ActionApi;
                HttpMethodType = dictionaryAttribute.HttpMethodType;
                ApiVersion = dictionaryAttribute.ApiVersion;
                ServiceName = StaticDocument.DocumentHandler.GetServiceName(dictionaryAttribute.ServiceName);
            }

            if (StaticDocument.DocumentHandler.DictionaryConfig != null && dictionaryAttribute.ActionApi.IsNullOrEmpty() && dictionaryAttribute.ServiceName.IsNullOrEmpty())
            {
                ActionApi = StaticDocument.DocumentHandler.DictionaryConfig.ActionApi;
                ApiVersion = StaticDocument.DocumentHandler.DictionaryConfig.ApiVersion;
                HttpMethodType = StaticDocument.DocumentHandler.DictionaryConfig.HttpMethodType;
                ServiceName = StaticDocument.DocumentHandler.DictionaryConfig.ServiceName;
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
