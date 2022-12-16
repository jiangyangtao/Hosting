using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yangtao.Hosting.Attribute;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Swagger.Filters
{
    internal class SwaggerSchemaFilter : ISchemaFilter
    {
        private Type SwaggerSchemaAttributeType = typeof(SwaggerSchemaAttribute);
        private Type SwaggerSchemaValidationAttributeType = typeof(SwaggerSchemaValidationAttribute);

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            #region 处理 Type/Format

            if (context.MemberInfo != null && context.MemberInfo.CustomAttributes.Count() > 0)
            {
                var swaggerSchemaType = context.MemberInfo.CustomAttributes.FirstOrDefault(a => a.AttributeType == SwaggerSchemaAttributeType);
                if (swaggerSchemaType == null) context.MemberInfo.CustomAttributes.FirstOrDefault(a => a.AttributeType == SwaggerSchemaValidationAttributeType);
                if (swaggerSchemaType != null)
                {
                    var format = swaggerSchemaType.NamedArguments.FirstOrDefault(a => a.MemberName == "Format");
                    if (format.TypedValue.Value != null) schema.Format = format.TypedValue.Value.ToString();

                    var type = swaggerSchemaType.NamedArguments.FirstOrDefault(a => a.MemberName == "Type");
                    if (type.TypedValue.Value != null) schema.Type = type.TypedValue.Value.ToString();
                }
            }

            #endregion

            #region 处理枚举

            var enumType = context.Type;
            var underlyingType = Nullable.GetUnderlyingType(enumType);
            if (enumType.IsEnum == false)
            {
                if (underlyingType == null) return;
                if (underlyingType.IsEnum == false) return;

                enumType = underlyingType;
            }

            var fields = enumType.GetFields().Where(a => a.FieldType == enumType).ToArray();
            if (fields.NotNullAndEmpty() && fields.FirstOrDefault().CustomAttributes.Any())
            {
                var strBuilder = new StringBuilder();
                for (int i = 0; i < fields.Length; i++)
                {
                    var field = fields[i];
                    var displayType = field.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DisplayAttribute));
                    if (displayType != null)
                    {
                        var name = displayType.NamedArguments.FirstOrDefault(a => a.MemberName == "Name");
                        strBuilder.Append($"{field.Name} {name.TypedValue.Value}");

                        if (i < fields.Length - 1) strBuilder.Append("，");
                    }
                }

                var description = strBuilder.ToString();
                if (description.NotNullAndEmpty()) schema.Description = $"{schema.Description}：{description}";
            }
            else schema.Description = $"{schema.Description}：{string.Join(",", Enum.GetNames(enumType))}";


            schema.Enum = Enum.GetNames(enumType).Select(value => new OpenApiString(value)).Cast<IOpenApiAny>().ToList();
            schema.Format = null;
            schema.Type = "string";

            #endregion
        }
    }
}
