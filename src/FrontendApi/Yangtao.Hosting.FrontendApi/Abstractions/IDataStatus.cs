using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.FrontendApi.Fields;

namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    internal interface IDataStatus
    {
        public Type? RequestData { get; }

        public string? EnableAction { get; }

        public string? DisableAction { get; }

        public HttpMethodType HttpActionType { get; }
    }

    internal class DataStatus : IDataStatus
    {
        private readonly IEnumerable<FieldBase> Params;

        public DataStatus()
        {
            Params = new Collection<FieldBase>();
        }

        public DataStatus(Type propertyType, XmlDocumentHandler xmlHandler) : this()
        {
            var dataStatusAttribute = propertyType.GetCustomAttribute<DataStatusAttribute>() ??
                throw new ArgumentException($"{nameof(propertyType)} not exist {nameof(DataStatusAttribute)}", nameof(propertyType));

            RequestData = dataStatusAttribute.RequestData;
            EnableAction = dataStatusAttribute.EnableAction;
            DisableAction = dataStatusAttribute.DisableAction;
            HttpActionType = dataStatusAttribute.HttpActionType;            
        }

        [JsonIgnore]
        public Type? RequestData { set; get; }

        public IEnumerable<FieldBase> RequestParams { set; get; }

        public string? EnableAction { set; get; }

        public string? DisableAction { set; get; }

        public HttpMethodType HttpActionType { set; get; }
    }
}
