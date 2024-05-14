using System.Collections.ObjectModel;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Controls;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.FrontendApi.Fields;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi
{
    internal static class Extensions
    {
        public static IHttpAction GetHttpAction(this MethodInfo method)
        {
            var controller = method.DeclaringType.Name.RemoveController();

            (HttpMethodType httpType, bool isDefault) = method.GetHttpType();
            if (isDefault) return new HttpAction(httpType, controller);

            return new HttpAction(httpType, $"{controller}/{method.Name}");
        }

        private static (HttpMethodType httpType, bool isDefault) GetHttpType(this MethodInfo method)
        {
            var httpGet = method.GetCustomAttribute(StaticKeys.HttpGetType);
            if (httpGet != null) return (HttpMethodType.Get, false);

            var httpDefaultGet = method.GetCustomAttribute(StaticKeys.HttpDefaultGetType);
            if (httpDefaultGet != null) return (HttpMethodType.Get, true);



            var httpPost = method.GetCustomAttribute(StaticKeys.HttpPostType);
            if (httpPost != null) return (HttpMethodType.Post, false);

            var httpDefaultPost = method.GetCustomAttribute(StaticKeys.HttpDefaultPostType);
            if (httpDefaultPost != null) return (HttpMethodType.Post, true);



            var httpPut = method.GetCustomAttribute(StaticKeys.HttpPutType);
            if (httpPut != null) return (HttpMethodType.Put, false);

            var httpDefaultPut = method.GetCustomAttribute(StaticKeys.HttpDefaultPutType);
            if (httpDefaultPut != null) return (HttpMethodType.Put, true);



            var httpPatch = method.GetCustomAttribute(StaticKeys.HttpPatchType);
            if (httpPatch != null) return (HttpMethodType.Patch, false);

            var httpDefaultPatch = method.GetCustomAttribute(StaticKeys.HttpDefaultPatchType);
            if (httpDefaultPatch != null) return (HttpMethodType.Patch, true);



            var httpDelete = method.GetCustomAttribute(StaticKeys.HttpDeleteType);
            if (httpDelete != null) return (HttpMethodType.Delete, false);

            var httpDefaultDelete = method.GetCustomAttribute(StaticKeys.HttpDefaultDeleteType);
            if (httpDefaultDelete != null) return (HttpMethodType.Delete, true);

            throw new ArgumentException($"{method.Name} not exist http method the attribute");
        }

        public static FieldType GetFieldType(this Type propertyType)
        {
            if (propertyType == StaticKeys.IntType || propertyType == StaticKeys.IntNullableType) return FieldType.Integer;
            if (propertyType == StaticKeys.DecimalType || propertyType == StaticKeys.DecimalNullableType ||
                    propertyType == StaticKeys.DoubleType || propertyType == StaticKeys.DoubleNullableType ||
                    propertyType == StaticKeys.FloatType || propertyType == StaticKeys.FloatNullableType) return FieldType.Decimal;

            if (propertyType == StaticKeys.DateTimeType || propertyType == StaticKeys.DateTimeNullableType) return FieldType.DateTime;
            if (propertyType.IsEnum) return FieldType.Enum;
            if (propertyType == StaticKeys.BooleanType || propertyType == StaticKeys.BooleanNullableType) return FieldType.Boolean;

            return FieldType.String;
        }

        public static IEnumerable<ControlBase> BuildControls(this Type formType, XmlDocumentHandler xmlHandler)
        {
            var properties = formType.GetProperties();
            var controls = new List<ControlBase>();
            var rangeControlCollection = new RangeControlCollection();
            foreach (var property in properties)
            {
                if (property.DeclaringType == StaticKeys.PaginationType) continue;

                var fieldType = GetFieldType(property.PropertyType);
                if (fieldType == FieldType.DateTime)
                {
                    var rangeAttr = property.GetCustomAttribute<RangePickerAttribute>();
                    if (rangeAttr != null)
                    {
                        rangeControlCollection.AddRangeControl(rangeAttr, property);
                        continue;
                    }

                    var endDateAttr = property.GetCustomAttribute<RangeEndDateAttribute>();
                    if (endDateAttr != null)
                    {
                        rangeControlCollection.AddRangeEndDateControl(endDateAttr, property);
                        continue;
                    }
                }

                var control = property.BuildControl(fieldType, xmlHandler);
                controls.Add(control);
            }

            var rangeControls = rangeControlCollection.BuildRangePickerControls(xmlHandler);
            if (rangeControls.NotNullAndEmpty()) controls.AddRange(rangeControls);

            return controls;
        }

        public static ControlBase BuildControl(this PropertyInfo property, XmlDocumentHandler xmlHandler)
        {
            var fieldType = GetFieldType(property.PropertyType);
            return property.BuildControl(fieldType, xmlHandler);
        }

        private static ControlBase BuildControl(this PropertyInfo property, FieldType fieldType, XmlDocumentHandler xmlHandler)
        {
            if (fieldType == FieldType.Enum)
            {
                var switchAttr = property.GetCustomAttribute<SwitchAttribute>();
                if (switchAttr != null) return new SwitchControl(switchAttr, property, xmlHandler);

                var selectAttr = property.GetCustomAttribute<SelectAttribute>();
                if (selectAttr != null) return new SelectControl(selectAttr, property, xmlHandler);

                return new SelectControl(property, xmlHandler);
            }

            if (fieldType == FieldType.Boolean) return new SwitchControl(property, xmlHandler);
            if (fieldType == FieldType.DateTime) return new DatePickerControl(property, xmlHandler);

            if (fieldType == FieldType.Decimal || fieldType == FieldType.Integer) return new InputNumberControl(property, xmlHandler);

            var textArea = property.GetCustomAttribute<TextAreaAttribute>();
            if (textArea != null) return new TextAreaControl(textArea, property, xmlHandler);

            var uploadAttr = property.GetCustomAttribute<UploadAttribute>();
            if (uploadAttr != null) return new UploadControl(uploadAttr, property, xmlHandler);

            var passwordAttr = property.GetCustomAttribute<PasswordAttribute>();
            if (passwordAttr != null) return new PasswordControl(passwordAttr, property, xmlHandler);

            return new InputControl(property, xmlHandler);
        }

        public static IEnumerable<TextValueOption> GetValueOptions(this Type enumType, XmlDocumentHandler xmlHandler)
        {
            if (enumType.IsEnum == false) return TextValueOption.Empty();

            var options = new Collection<TextValueOption>();
            var fields = enumType.GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType.IsEnum == false) continue;

                var text = xmlHandler.GetFieldSummary(field);
                var textValueOption = new TextValueOption(field.Name, text);
                options.Add(textValueOption);
            }

            return options;
        }

        public static string RemoveController(this string controller) => controller.Replace(StaticKeys.ControllerKey, "");

        public static IEnumerable<FieldBase> BuildFields(this Type dataType, XmlDocumentHandler xmlHandler)
        {
            return dataType.GetProperties().Select(a => a.BuildField(xmlHandler));
        }

        public static IEnumerable<ParamField> BuildParamFields(this Type dataType, XmlDocumentHandler xmlHandler)
        {
            return dataType.GetProperties().Select(property => new ParamField(property, xmlHandler));
        }

        public static FieldBase BuildField(this PropertyInfo property, XmlDocumentHandler xmlHandler)
        {
            var fieldType = property.PropertyType.GetFieldType();
            if (fieldType == FieldType.Enum) return new EnumField(property, xmlHandler);

            return new Field(fieldType, property, xmlHandler);
        }
    }
}
