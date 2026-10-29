using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yangtao.Hosting.Logger.Options;

namespace Yangtao.Hosting.Logger
{
    public class RealtimeLoggerResponse
    {
        public RealtimeLoggerResponse() { }

        public RealtimeLoggerResponse(RealtimeLoggerResponseType responseType, string? result)
        {
            ResponseType = responseType;
            Result = result;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public RealtimeLoggerResponseType ResponseType { set; get; }

        public string? Result { set; get; }

        public string ToJson() => JsonConvert.SerializeObject(this);

        public override string ToString() => $"{ResponseType}:{Result} \n";

        public static RealtimeLoggerResponse Event(string result) => new(RealtimeLoggerResponseType.Event, result);

        public static RealtimeLoggerResponse Data(string result) => new(RealtimeLoggerResponseType.Data, result);

        public static RealtimeLoggerResponse Connectioned() => Event("Connected");

        public static RealtimeLoggerResponse KeepAlive() => Event("keep-alive");
    }
}
