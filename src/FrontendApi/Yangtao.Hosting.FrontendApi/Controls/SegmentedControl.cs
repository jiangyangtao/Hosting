using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
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
            if (segmentedAttribute.IsValid == false) throw new InvalidOperationException($"{property.Name} the SegmentedAttribute http action api not config.");

            SourceType = segmentedAttribute.SourceType;
            ActionApi = segmentedAttribute.ActionApi;
            HttpMethodType = segmentedAttribute.HttpMethodType;
            ApiVersion = segmentedAttribute.ApiVersion;
            ServiceName = StaticDocument.DocumentHandler.GetServiceName(segmentedAttribute.ServiceName);
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Segmented;

        public bool Block { set; get; } = false;

        [JsonConverter(typeof(StringEnumConverter))]
        public SelectSourceType SourceType { set; get; }

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Get;

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; }

        public string? ServiceName { set; get; }
    }
}
