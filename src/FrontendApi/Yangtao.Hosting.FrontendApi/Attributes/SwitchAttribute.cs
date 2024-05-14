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

        internal bool IsEmpty
        {
            get
            {
                if (CheckedValue != null) return false;
                if (UnCheckedValue != null) return false;
                if (CheckedChildren != null) return false;
                if (UnCheckedChildren != null) return false;

                return true;
            }
        }
    }
}
