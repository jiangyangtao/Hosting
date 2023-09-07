
namespace Yangtao.Hosting.Enum
{
    public class EnumProvider<TEnum> : EnumHandlerBase where TEnum : struct, System.Enum
    {
        public EnumProvider() : base(typeof(TEnum))
        {
        }

        public IEnumerable<TEnum> GetValues() => System.Enum.GetValues<TEnum>();
    }
}
