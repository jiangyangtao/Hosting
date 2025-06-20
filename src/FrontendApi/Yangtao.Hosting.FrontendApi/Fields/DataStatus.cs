using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Fields
{
    internal class DataStatus : FieldBase, IDataStatus
    {
        public DataStatus(PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            RequestParams = new Collection<ParamField>();

            var dataStatusAttribute = property.GetCustomAttribute<DataStatusAttribute>() ??
                throw new ArgumentException($"{nameof(property)} not exist {nameof(DataStatusAttribute)}", nameof(property));

            RequestData = dataStatusAttribute.RequestData;

            var enableHttpAction = new HttpAction(dataStatusAttribute.EnableHttpAction);
            enableHttpAction.ServiceName = documentHandler.GetServiceName(enableHttpAction.ServiceName);

            var disableHttpAction = new HttpAction(dataStatusAttribute.DisableHttpAction);
            disableHttpAction.ServiceName = documentHandler.GetServiceName(disableHttpAction.ServiceName);

            EnableHttpAction = enableHttpAction;
            DisableHttpAction = disableHttpAction;

            if (RequestData != null) RequestParams = RequestData.BuildParamFields(documentHandler);
        }

        [JsonIgnore]
        public Type? RequestData { set; get; }

        public IEnumerable<ParamField> RequestParams { set; get; }

        public IHttpAction EnableHttpAction { get; }

        public IHttpAction DisableHttpAction { get; }

        public override FieldType FieldType => FieldType.Enum;
    }
}
