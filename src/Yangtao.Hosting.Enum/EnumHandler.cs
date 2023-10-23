
namespace Yangtao.Hosting.Enum
{
    public class EnumHandler<TEnum> : EnumHandlerBase, IDisposable where TEnum : struct, System.Enum
    {
        private EnumHandler() : base(typeof(TEnum))
        {

        }

        public static EnumHandler<TEnum> Create() => new();

        public IEnumerable<TEnum> GetValues() => System.Enum.GetValues<TEnum>();

        public TEnum? GetEnumValue(string name) => GetValue<TEnum>(name);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
