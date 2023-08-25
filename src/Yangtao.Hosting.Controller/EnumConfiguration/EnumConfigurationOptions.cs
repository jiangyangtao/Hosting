using System.Net.Mime;

namespace Yangtao.Hosting.Controller.EnumConfiguration
{
    public class EnumConfigurationOptions
    {
        public EnumConfigurationOptions()
        {
        }

        /// <summary>
        /// Get EnumConfiguration the endpoint,Default "/EnumConfiguration"
        /// </summary>
        public string EnumConfigurationEndpoint { set; get; } = "/EnumConfiguration";

        public bool RequireAuthorization { set; get; } = true;

        public bool IncludeNumericField { set; get; } = false;

        /// <summary>
        /// Enum the Name Attribute,Default "Text"
        /// </summary>
        public string EnumNameField { set; get; } = "Text";

        /// <summary>
        /// Enum the Value,Default "Value"
        /// </summary>
        public string EnumValueField { set; get; } = "Value";

        /// <summary>
        /// Enum the Number,Default "Number"
        /// </summary>
        public string EnumNumericField { set; get; } = "Number";

        /// <summary>
        /// Enum Result the ContentType,Default application/json
        /// </summary>
        public string ResponseContentType { set; get; } = MediaTypeNames.Application.Json;
    }
}
