using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    /// <summary>
    /// 前辍/后辍控件基类
    /// </summary>
    internal abstract class PrefixSuffixControlBase : ControlBase
    {
        protected PrefixSuffixControlBase(PropertyInfo property) : base(property)
        {
            var prefixAttr = property.GetCustomAttribute<PrefixAttribute>();
            if (prefixAttr != null)
            {
                Prefix = prefixAttr.Value;
                PrefixType = prefixAttr.PrefixType;
            }

            var suffixAttr = property.GetCustomAttribute<SuffixAttribute>();
            if (suffixAttr != null)
            {
                Suffix = suffixAttr.Value;
                SuffixType = suffixAttr.SuffixType;
            }

            var addOnBeforeAttr = property.GetCustomAttribute<AddOnBeforeAttribute>();
            if (addOnBeforeAttr != null)
            {
                AddOnBefore = addOnBeforeAttr.Value;
                AddOnBeforeType = addOnBeforeAttr.AddOnBeforeType;

                if (addOnBeforeAttr.AddOnBeforeType == AddOnType.Enum && addOnBeforeAttr.AddOnBeforeSource != null)
                    AddOnBeforeOptions = addOnBeforeAttr.AddOnBeforeSource.GetValueOptions();
            }

            var addOnAfterAttr = property.GetCustomAttribute<AddOnAfterAttribute>();
            if (addOnAfterAttr != null)
            {
                AddOnAfter = addOnAfterAttr.Value;
                AddOnAfterType = addOnAfterAttr.AddOnAfterType;

                if (addOnAfterAttr.AddOnAfterType == AddOnType.Enum && addOnAfterAttr.AddOnAfterSource != null)
                    AddOnAfterOptions = addOnAfterAttr.AddOnAfterSource.GetValueOptions();
            }
        }

        /// <summary>
        /// 前缀
        /// </summary>
        public string? Prefix { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AffixType? PrefixType { set; get; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string? Suffix { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AffixType? SuffixType { set; get; }

        /// <summary>
        /// 后置
        /// </summary>
        public string? AddOnBefore { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AddOnType? AddOnBeforeType { set; get; }

        public IEnumerable<TextValueOption> AddOnBeforeOptions { set; get; } = TextValueOption.Empty();

        /// <summary>
        /// 后置
        /// </summary>
        public string? AddOnAfter { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AddOnType? AddOnAfterType { set; get; }

        public IEnumerable<TextValueOption> AddOnAfterOptions { set; get; } = TextValueOption.Empty();
    }
}
