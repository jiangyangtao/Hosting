using System.Net.Mime;

namespace Yangtao.Hosting.FrontendApi
{
    public class FrontendApiConfigurationOptions
    {
        /// <summary>
        /// Get Frontend Configuration the endpoint,Default "//Frontend/Configuration"
        /// </summary>
        public string Endpoint { set; get; } = "/Frontend/Configuration";

        public bool RequireAuthorization { set; get; } = true;

        public string? DefaultServiceName { set; get; }

        /// <summary>
        /// Enum Result the ContentType,Default application/json
        /// </summary>
        public string ResponseContentType { set; get; } = MediaTypeNames.Application.Json;


        public DictionaryConfig? DictionaryConfig { set; get; }
    }
}
