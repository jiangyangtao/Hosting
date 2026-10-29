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

        public override string ToString()
        {
            if (ResponseType == RealtimeLoggerResponseType.Log) return $"{ResponseType}: \n{Result}\n";

            return $"{ResponseType}:{Result} \n \n";
        }

        public static RealtimeLoggerResponse Log(string result) => new(RealtimeLoggerResponseType.Log, result);

        public static RealtimeLoggerResponse State(string result) => new(RealtimeLoggerResponseType.State, result);

        public static RealtimeLoggerResponse Connectioned() => State("connectioned");

        public static RealtimeLoggerResponse KeepAlive() => State("keep-alive");
    }
}
