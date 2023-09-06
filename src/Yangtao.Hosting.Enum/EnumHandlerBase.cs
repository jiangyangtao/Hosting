namespace Yangtao.Hosting.Enum
{
    public abstract class EnumHandlerBase
    {
        protected readonly Type EnumType;

        protected EnumHandlerBase(Type enumType)
        {

        }

        public virtual string[] Names => System.Enum.GetNames(EnumType);

        public virtual int[] Values => System.Enum.GetValues(EnumType);

        public bool HasName(string name) => Names.Any(a => a == name);

        public bool HasValue(int value) => Values.Any(a => a == value);

        public bool HasEnum<TEnum>(TEnum @enum) where TEnum : struct, System.Enum
        {

        }

        public int GetValue(string name)
        {

        }

        public TEnum GetValue<TEnum>(string name) where TEnum : struct, System.Enum
        {

        }

        public string GetName(int value)
        {

        }

        public TEnum GetName<TEnum>(int value) where TEnum : struct, System.Enum
        {

        }
    }


}