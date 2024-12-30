using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class InputNumberControl : ControlBase
    {
        public InputNumberControl(FieldType fieldType, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            ChangeStep = 1;
            Min = 0;
            Max = int.MaxValue;
            if (fieldType == FieldType.Decimal)
            {
                ChangeStep = 0.1;
                Max = decimal.MaxValue;
            }


            var rangeAttr = property.GetCustomAttribute<RangeAttribute>();
            if (rangeAttr != null)
            {
                Min = rangeAttr.Minimum;
                Max = rangeAttr.Maximum;
            }

            var percentageRangeAttr = property.GetCustomAttribute<PercentageRangeAttribute>(false);
            if (percentageRangeAttr != null)
            {
                Min = percentageRangeAttr.Minimum;
                Max = percentageRangeAttr.Maximum;
            }

            var inputNumberAttr = property.GetCustomAttribute<InputNumberAttribute>();
            if (inputNumberAttr != null)
            {
                ChangeStep = inputNumberAttr.Step;
                Keyboard = inputNumberAttr.Keyboard;
                ShowNumberControls = inputNumberAttr.ShowNumberControls;
                Bordered = inputNumberAttr.Bordered;
            }

            var prefixAttr = property.GetCustomAttribute<PrefixAttribute>();
            if (prefixAttr != null)
            {
                Prefix = prefixAttr.Value;
                PrefixType = prefixAttr.PrefixType;
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

        public bool ShowNumberControls { set; get; } = true;

        public bool Keyboard { set; get; } = true;

        public bool Bordered { set; get; } = true;

        public object? ChangeStep { set; get; }

        public object? Max { set; get; }

        public object? Min { set; get; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string? Prefix { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AffixType? PrefixType { set; get; }


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
        public override ControlType ControlType => ControlType.InputNumber;
    }
}
