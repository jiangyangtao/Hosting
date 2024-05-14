using System.ComponentModel.DataAnnotations;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PercentageRangeAttribute : RangeAttribute
    {
        public PercentageRangeAttribute() : base(0, 100)
        {
        }

        public PercentageRangeAttribute(int min) : base(min, 100)
        {
        }

        public PercentageRangeAttribute(double min) : base(min, 100)
        {
        }
    }
}
