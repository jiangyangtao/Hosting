using Yangtao.Hosting.FrontendApi.Abstractions;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwitchAttribute : Attribute, ISwitch
    {
        public SwitchAttribute()
        {
        }

        public SwitchAttribute(object checkedValue, object unCheckedValue, object checkedChildren, object unCheckedChildren)
        {
            CheckedValue = checkedValue;
            UnCheckedValue = unCheckedValue;
            CheckedChildren = checkedChildren;
            UnCheckedChildren = unCheckedChildren;
        }

        public object? CheckedValue { set; get; }

        public object? UnCheckedValue { set; get; }

        public object? CheckedChildren { set; get; }

        public object? UnCheckedChildren { set; get; }

        public object? DefaultValue { set; get; }

        internal bool IsValid
        {
            get
            {
                if (CheckedValue != null) return true;
                if (UnCheckedValue != null) return true;
                if (CheckedChildren != null) return true;
                if (UnCheckedChildren != null) return true;

                return false;
            }
        }
    }
}
