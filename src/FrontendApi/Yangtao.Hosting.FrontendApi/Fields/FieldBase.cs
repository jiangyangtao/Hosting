using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Fields
{
    internal abstract class FieldBase : IField
    {
        protected FieldBase()
        {
        }

        protected FieldBase(PropertyInfo property, DocumentHandler documentHandler)
        {

            Name = property.Name;
            Text = documentHandler.GetPropertySummary(property);

            var formatAttribute = property.GetCustomAttribute<FormatterAttribute>();
            if (formatAttribute != null) Format = formatAttribute.Formatter;

            var uniqueKey = property.GetCustomAttribute<UniqueKeyAttribute>();
            if (uniqueKey != null) IsKey = true;

            var fieldItemAttribute = property.GetCustomAttribute<FieldGroupItemAttribute>();
            if (fieldItemAttribute != null)
            {
                GroupName = fieldItemAttribute.GroupName;
            }
        }

        public string? Name { protected set; get; }

        public string? Text { protected set; get; }

        public string? Format { protected set; get; }

        public abstract FieldType FieldType { get; }

        public string? GroupName { protected set; get; }

        public bool IsKey { protected set; get; }
    }
}
