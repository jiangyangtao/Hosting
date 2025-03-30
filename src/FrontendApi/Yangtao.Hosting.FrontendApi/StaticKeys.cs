using Microsoft.AspNetCore.Mvc;
using Yangtao.Hosting.Common;
using Yangtao.Hosting.Mvc.FormatResult;
using Yangtao.Hosting.Mvc.HttpMethods;

namespace Yangtao.Hosting.FrontendApi
{
    internal class StaticKeys
    {
        public const string ControllerKey = "Controller";

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


        public static readonly Type PaginationType = typeof(PagingParameter);
        public static readonly Type NullableType = typeof(Nullable<>);


        public static readonly Type HttpGetType = typeof(HttpGetAttribute);
        public static readonly Type HttpDefaultGetType = typeof(HttpGetDefaultAttribute);

        public static readonly Type HttpPostType = typeof(HttpPostAttribute);
        public static readonly Type HttpDefaultPostType = typeof(HttpPostDefaultAttribute);

        public static readonly Type HttpPutType = typeof(HttpPutAttribute);
        public static readonly Type HttpDefaultPutType = typeof(HttpPutDefaultAttribute);

        public static readonly Type HttpPatchType = typeof(HttpPatchAttribute);
        public static readonly Type HttpDefaultPatchType = typeof(HttpPatchDefaultAttribute);

        public static readonly Type HttpDeleteType = typeof(HttpDeleteAttribute);
        public static readonly Type HttpDefaultDeleteType = typeof(HttpDeleteDefaultAttribute);

    }
}
