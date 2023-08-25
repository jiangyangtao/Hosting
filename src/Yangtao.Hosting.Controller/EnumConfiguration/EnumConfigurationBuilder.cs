using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Controller.EnumConfiguration
{
    internal class EnumConfigurationBuilder
    {
        private const string ProjectType = "project";
        private const string DescriptionKey = "Description";
        private const string NameKey = "Name";

        private readonly Dictionary<string, XDocument> XmlDocmentDictionary;
        private readonly EnumConfigurationOptions enumConfigurationOptions;

        public EnumConfigurationBuilder(EnumConfigurationOptions configurationOptions)
        {
            enumConfigurationOptions = configurationOptions;
            XmlDocmentDictionary = new();
        }

        public string GetEnumConfiguration()
        {
            var assemblys = DependencyContext.Default.CompileLibraries.Where(a => a.Type == ProjectType).Select(a => Assembly.Load(a.Name)).ToArray();
            var enumArray = new JArray();
            foreach (var assembly in assemblys)
            {
                var enumTypes = assembly.GetTypes().Where(a => a.IsEnum).ToArray();
                if (enumTypes.IsNullOrEmpty()) continue;

                foreach (var enumType in enumTypes)
                {
                    var fields = enumType.GetFields();
                    var fieldArray = BuildFieldArray(fields);

                    if (fieldArray == null) continue;
                    var enumName = $"T:{enumType.FullName}";
                    var enumDescription = GetSummaryText(assembly.GetName().Name, enumType.FullName, false);
                    var enumObject = new JObject
                    {
                        { "EnumName", enumType.Name },
                        { "Description",enumDescription},
                        { "Values",fieldArray}
                    };
                    enumArray.Add(enumObject);
                }
            }

            return JsonConvert.SerializeObject(enumArray);
        }

        private JArray? BuildFieldArray(FieldInfo[] fields)
        {
            var fieldArray = new JArray();
            foreach (var field in fields)
            {
                if (field.FieldType.IsEnum == false) continue;

                var memberName = $"{field.FieldType.FullName}.{field.Name}";
                var text = GetSummaryText(field.Module.Assembly.GetName().Name, memberName);
                var item = new JObject
                            {
                                { enumConfigurationOptions.EnumValueField, field.Name },
                                { enumConfigurationOptions.EnumNameField,text },
                            };

                if (enumConfigurationOptions.IncludeNumericField)
                {
                    var number = (int)field.GetValue(null);
                    item.Add(enumConfigurationOptions.EnumNumericField, number);
                }

                if (field.CustomAttributes.Any())
                {
                    var displayType = field.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DisplayAttribute));
                    if (displayType != null)
                    {
                        var name = displayType.NamedArguments.FirstOrDefault(a => a.MemberName == NameKey);
                        item[enumConfigurationOptions.EnumNameField] = name.TypedValue.Value.ToString();
                    }

                    var descriptionType = field.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DescriptionAttribute));
                    if (descriptionType != null)
                    {
                        var name = descriptionType.NamedArguments.FirstOrDefault(a => a.MemberName == DescriptionKey);
                        item.Add(DescriptionKey, name.TypedValue.Value.ToString());
                    }
                }

                fieldArray.Add(item);
            }

            if (fieldArray.Count <= 0) return null;

            return fieldArray;
        }

        private string? GetSummaryText(string assemblyName, string memberName, bool isField = true)
        {
            var xmlDoc = GetXmlDocument(assemblyName);
            if (xmlDoc == null) return string.Empty;

            var abbreviations = isField ? "F" : "T";
            var memberFullName = $"{abbreviations}:{memberName}";

            return xmlDoc.XPathEvaluate($"normalize-space(//member[@name = '{memberFullName}']/summary/text())") as string;
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
