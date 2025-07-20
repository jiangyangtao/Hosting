namespace Yangtao.Hosting.Abstractions
{
    public class BasicTypes
    {
        public static readonly Type StringType = typeof(string);
        public static readonly Type ShortType = typeof(short);
        public static readonly Type ShortNullableType = typeof(short?);
        public static readonly Type IntType = typeof(int);
        public static readonly Type IntNullableType = typeof(int?);
        public static readonly Type Int64Type = typeof(long);
        public static readonly Type Int64NullableType = typeof(long?);
        public static readonly Type FloatType = typeof(float);
        public static readonly Type FloatNullableType = typeof(float?);
        public static readonly Type DoubleType = typeof(double);
        public static readonly Type DoubleNullableType = typeof(double?);
        public static readonly Type DecimalType = typeof(decimal);
        public static readonly Type DecimalNullableType = typeof(decimal?);
        public static readonly Type DateTimeType = typeof(DateTime);
        public static readonly Type DateTimeNullableType = typeof(DateTime?);
        public static readonly Type BooleanType = typeof(bool);
        public static readonly Type BooleanNullableType = typeof(bool?);
        public static readonly Type GuidType = typeof(Guid);
        public static readonly Type GuidNullableType = typeof(Guid?);

        public static bool IsStringType(Type type) => type == StringType;

        public static bool IsShortType(Type type) => type == ShortType || type == ShortNullableType;

        public static bool IsIntType(Type type) => type == IntType || type == IntNullableType;

        public static bool IsLongType(Type type) => type == Int64Type || type == Int64NullableType;

        public static bool IsFloatType(Type type) => type == FloatType || type == FloatNullableType;

        public static bool IsDoubleType(Type type) => type == DoubleType || type == DoubleNullableType;

        public static bool IsDecimalType(Type type) => type == DecimalType || type == DecimalNullableType;

        public static bool IsDateTimeType(Type type) => type == DateTimeType || type == DateTimeNullableType;

        public static bool IsBooleanType(Type type) => type == BooleanType || type == BooleanNullableType;

        public static bool IsGuidType(Type type) => type == GuidType || type == GuidNullableType;


        public static bool IsNumeric(Type type)
        {
            if (IsShortType(type)) return true;
            if (IsIntType(type)) return true;
            if (IsLongType(type)) return true;
            if (IsFloatType(type)) return true;
            if (IsDoubleType(type)) return true;
            if (IsDecimalType(type)) return true;

            return false;
        }
    }
}
