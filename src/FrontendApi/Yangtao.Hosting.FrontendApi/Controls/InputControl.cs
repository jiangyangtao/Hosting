using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class InputControl : ControlBase
    {
        public InputControl(PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            var maxLenthAttr = property.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLenthAttr != null) MaxLength = maxLenthAttr.Length;

            var inputAttr = property.GetCustomAttribute<InputAttribute>();
            if (inputAttr != null)
            {
                AllowClear = inputAttr.AllowClear;
                ShowCount = inputAttr.ShowCount;
                Bordered = inputAttr.Bordered;
            }

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
                SuffixType = suffixAttr.PrefixType;
            }

            var addOnBeforeAttr = property.GetCustomAttribute<AddOnBeforeAttribute>();
            if (addOnBeforeAttr != null)
            {
                AddOnBefore = addOnBeforeAttr.Value;
                AddOnBeforeType = addOnBeforeAttr.AddOnBeforeType;

                if (addOnBeforeAttr.AddOnBeforeType == AddOnType.Enum && addOnBeforeAttr.AddOnBeforeSource != null)
                    AddOnBeforeOptions = addOnBeforeAttr.AddOnBeforeSource.GetValueOptions(documentHandler);
            }

            var addOnAfterAttr = property.GetCustomAttribute<AddOnAfterAttribute>();
            if (addOnAfterAttr != null)
            {
                AddOnAfter = addOnAfterAttr.Value;
                AddOnAfterType = addOnAfterAttr.AddOnAfterType;

                if (addOnAfterAttr.AddOnAfterType == AddOnType.Enum && addOnAfterAttr.AddOnAfterSource != null)
                    AddOnAfterOptions = addOnAfterAttr.AddOnAfterSource.GetValueOptions(documentHandler);
            }
        }

        public int? MaxLength { set; get; }

        public bool ShowCount { set; get; } = true;

        public bool AllowClear { set; get; } = true;

        public bool Bordered { set; get; } = true;

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

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Input;
    }
}
