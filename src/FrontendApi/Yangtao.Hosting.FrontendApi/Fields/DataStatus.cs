using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Fields
{
    internal class DataStatus : FieldBase, IDataStatus
    {
        public DataStatus(PropertyInfo property) : base(property)
        {
            var fieldType = property.GetFieldType();
            if (fieldType != FieldType.Enum && fieldType != FieldType.Boolean) throw new InvalidOperationException($"{property.Name} the DataStatus only supports Enum or Boolean.");

            FieldType = fieldType;
            RequestParams = Array.Empty<ParamField>();

            var dataStatusAttribute = property.GetCustomAttribute<DataStatusAttribute>() ??
                throw new InvalidOperationException($"{property.Name} not exist [DataStatusAttribute].");

            RequestData = dataStatusAttribute.RequestData;
                        
            var enableHttpAction = new HttpAction(dataStatusAttribute.EnableHttpAction);
            enableHttpAction.ServiceName = StaticDocument.DocumentHandler.GetServiceName(enableHttpAction.ServiceName);

            var disableHttpAction = new HttpAction(dataStatusAttribute.DisableHttpAction);
            disableHttpAction.ServiceName = StaticDocument.DocumentHandler.GetServiceName(disableHttpAction.ServiceName);

            EnableHttpAction = enableHttpAction;
            DisableHttpAction = disableHttpAction;

            if (RequestData != null) RequestParams = RequestData.BuildParamFields();
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Type? RequestData { set; get; }

        public IEnumerable<ParamField> RequestParams { set; get; }

        public IHttpAction EnableHttpAction { set; get; }

        public IHttpAction DisableHttpAction { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override FieldType FieldType { get; }
    }
}
