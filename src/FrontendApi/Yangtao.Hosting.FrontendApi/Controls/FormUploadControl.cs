using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    /// <summary>
    /// 表单上传控件
    /// <para>用于上传文件无字段的表单</para>
    /// </summary>
    internal class FormUploadControl : ControlBase
    {
        public FormUploadControl(FormUploadAttribute formUploadAttr)
        {
            Text = formUploadAttr.Text;
            Name = formUploadAttr.Name;
            UploadCount = formUploadAttr.UploadCount;
            Bordered = formUploadAttr.Bordered;
            Required = formUploadAttr.Required;
        }

        public FormUploadControl(PropertyInfo property) : base(property)
        {
        }

        public int UploadCount { set; get; } = 1;

        public bool Bordered { set; get; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Upload;
    }
}
