using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class FormUploadControl : ControlBase, IFieldGroup
    {
        public FormUploadControl(FormUploadAttribute formUploadAttr)
        {
            Text = formUploadAttr.Text;
            Name = formUploadAttr.Text;
            UploadCount = formUploadAttr.UploadCount;
            Bordered = formUploadAttr.Bordered;
            SortIndex = formUploadAttr.SortIndex;
            GroupName = formUploadAttr.GroupName;
            Required = formUploadAttr.Required;
        }

        public FormUploadControl(PropertyInfo property, XmlDocumentHandler xmlHandler) : base(property, xmlHandler)
        {
        }

        public int UploadCount { set; get; } = 1;

        public bool Bordered { set; get; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Upload;
    }
}
