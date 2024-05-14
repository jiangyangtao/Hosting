using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.FrontendApi
{
    internal class XmlDocumentHandler
    {
        private readonly Dictionary<string, XDocument> XmlDocmentDictionary;

        public XmlDocumentHandler()
        {
            XmlDocmentDictionary = new();
        }

        public string? GetFieldSummary(FieldInfo field)
        {
            var assemblyName = field.FieldType.Assembly.GetName().Name;
            var memberName = $"F:{field.FieldType.FullName}.{field.Name}";
            return GetSummaryText(assemblyName, memberName);
        }

        public string? GetModuleSummary(Type module)
        {
            var assemblyName = module.Assembly.GetName().Name;
            var memberName = $"T:{module.FullName}";

            return GetSummaryText(assemblyName, memberName);
        }

        public string? GetPropertySummary(PropertyInfo property)
        {
            var assemblyName = property.DeclaringType.Assembly.GetName().Name;
            var memberName = $"P:{property.DeclaringType.FullName}.{property.Name}";

            var text = GetSummaryText(assemblyName, memberName);
            if (property.PropertyType.IsEnum && text.IsNullOrEmpty()) return GetEnumSummary(property);

            return text;
        }

        public string? GetEnumSummary(PropertyInfo property)
        {
            var assemblyName = property.PropertyType.Assembly.GetName().Name;
            var memberName = $"T:{property.PropertyType.FullName}";
            return GetSummaryText(assemblyName, memberName);
        }

        private string? GetSummaryText(string assemblyName, string memberName)
        {
            var xmlDoc = GetXmlDocument(assemblyName);
            if (xmlDoc == null) return string.Empty;

            return xmlDoc.XPathEvaluate($"normalize-space(//member[@name = '{memberName}']/summary/text())") as string;
        }

        private XDocument? GetXmlDocument(string assemblyName)
        {
            if (XmlDocmentDictionary.ContainsKey(assemblyName)) return XmlDocmentDictionary[assemblyName];

            var xmlFile = $"{assemblyName}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            var existPath = File.Exists(xmlPath);
            if (existPath == false) return null;

            var xmlDoc = XDocument.Load(xmlPath);
            XmlDocmentDictionary.Add(assemblyName, xmlDoc);
            return xmlDoc;
        }
    }
}
