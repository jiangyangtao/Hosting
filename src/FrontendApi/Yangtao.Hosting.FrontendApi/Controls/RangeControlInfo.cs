using System.Collections.ObjectModel;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Attributes;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class RangeEndDateControlInfo
    {
        public RangeEndDateControlInfo(string rangeName, PropertyInfo property)
        {
            RangeName = rangeName;
            Property = property;
        }

        public string? RangeName { set; get; }

        public PropertyInfo Property { set; get; }
    }

    internal class RangeControlInfo : RangeEndDateControlInfo
    {
        public RangeControlInfo(RangePickerAttribute rangePickerAttr, PropertyInfo property) : base(rangePickerAttr.RangeName, property)
        {
            RangePickerAttr = rangePickerAttr;
        }

        public RangePickerAttribute RangePickerAttr { set; get; }
    }

    internal class RangeControlCollection
    {
        private readonly ICollection<RangeControlInfo> RangeControls;
        private readonly ICollection<RangeEndDateControlInfo> RangeEndDateControls;

        public RangeControlCollection()
        {
            RangeControls = new Collection<RangeControlInfo>();
            RangeEndDateControls = new Collection<RangeEndDateControlInfo>();
        }

        public void AddRangeControl(RangePickerAttribute rangePicker, PropertyInfo property)
        {
            var exist = RangeControls.Any(a => a.RangeName == rangePicker.RangeName);
            if (exist) return;

            var r = new RangeControlInfo(rangePicker, property);
            RangeControls.Add(r);
        }

        public void AddRangeEndDateControl(RangeEndDateAttribute rangeEndDate, PropertyInfo property)
        {
            var exist = RangeEndDateControls.Any(a => a.RangeName == rangeEndDate.RangeName);
            if (exist) return;

            var r = new RangeEndDateControlInfo(rangeEndDate.RangeName, property);
            RangeEndDateControls.Add(r);
        }

        public IEnumerable<RangePickerControl> BuildRangePickerControls(XmlDocumentHandler xmlHandler)
        {
            if (RangeControls.IsNullOrEmpty()) return Array.Empty<RangePickerControl>();

            var controls = new List<RangePickerControl>();
            foreach (var rangeControl in RangeControls)
            {
                var endDate = RangeEndDateControls.FirstOrDefault(a => a.RangeName == rangeControl.RangeName);
                if (endDate == null) continue;

                var rangePickerControl = new RangePickerControl(rangeControl, rangeControl, xmlHandler);
                controls.Add(rangePickerControl);
            }

            return controls;
        }
    }
}
