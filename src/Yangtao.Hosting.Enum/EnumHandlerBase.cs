
namespace Yangtao.Hosting.Enum
{
    public abstract class EnumHandlerBase
    {
        protected readonly Type EnumType;

        protected EnumHandlerBase(Type enumType)
        {
            var array = System.Enum.GetValues(enumType);
            var enumInfos = new List<EnumInfo>();
            foreach (var item in array)
            {
                enumInfos.Add(new EnumInfo
                {
                    Name = item.ToString(),
                    ObjectValue = item,
                    Value = (int)item,
                });
            }
            EnumInfos = enumInfos;
        }

        public IEnumerable<EnumInfo> EnumInfos { get; }

        public virtual IEnumerable<string> Names => EnumInfos.Select(a => a.Name);

        public virtual IEnumerable<int> Values => EnumInfos.Select(a => a.Value);

        public virtual IEnumerable<object> ObjectValues => EnumInfos.Select(a => a.ObjectValue);

        public bool HasName(string name) => Names.Any(a => a == name);

        public bool HasValue(int value) => Values.Any(a => a == value);

        public int? GetValue(string name)
        {
            var enumInfo = EnumInfos.FirstOrDefault(a => a.Name == name);
            if (enumInfo == null) return null;

            return enumInfo.Value;
        }

        public TEnum? GetValue<TEnum>(string name) where TEnum : struct, System.Enum
        {
            var enumInfo = EnumInfos.FirstOrDefault(a => a.Name == name);
            if (enumInfo == null) return null;

            return (TEnum)enumInfo.ObjectValue;
        }

        public string GetName(int value)
        {
            var enumInfo = EnumInfos.FirstOrDefault(a => a.Value == value);
            if (enumInfo == null) return string.Empty;

            return enumInfo.Name;
        }
    }


}