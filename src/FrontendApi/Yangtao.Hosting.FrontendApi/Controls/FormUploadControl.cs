using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class FormUploadControl : ControlBase
    {
        public FormUploadControl(FormUploadAttribute formUploadAttr)
        {
            Text = formUploadAttr.Text;
            Name = formUploadAttr.Text;
            UploadCount = formUploadAttr.UploadCount;
            Bordered = formUploadAttr.Bordered;
            GroupName = formUploadAttr.GroupName;
            Required = formUploadAttr.Required;
        }

        public FormUploadControl(PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
        }

        public int UploadCount { set; get; } = 1;

        public bool Bordered { set; get; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Upload;
    }
}
