using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Fields
{
    internal abstract class FieldBase : IField, IFieldGroup
    {
        protected FieldBase()
        {
        }

        protected FieldBase(PropertyInfo property, XmlDocumentHandler xmlHandler)
        {
            Name = property.Name;
            Text = xmlHandler.GetPropertySummary(property);

            var formatAttribute = property.GetCustomAttribute<FormatterAttribute>();
            if (formatAttribute != null) Format = formatAttribute.Formatter;

            var uniqueKey = property.GetCustomAttribute<UniqueKeyAttribute>();
            if (uniqueKey != null) IsKey = true;

            var fieldItemAttribute = property.GetCustomAttribute<FieldItemAttribute>();
            if (fieldItemAttribute != null)
            {
                SortIndex = fieldItemAttribute.SortIndex;
                GroupName = fieldItemAttribute.GroupName;
            }
        }

        public string? Name { protected set; get; }

        public string? Text { protected set; get; }

        public string? Format { protected set; get; }

        public abstract FieldType FieldType { get; }

        public int SortIndex { protected set; get; }

        public string? GroupName { protected set; get; }

        public bool IsKey { protected set; get; }
    }
}
