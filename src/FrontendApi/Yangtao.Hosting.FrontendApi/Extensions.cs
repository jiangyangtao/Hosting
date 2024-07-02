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
        public static IHttpAction GetHttpAction(this MethodInfo method, HttpVersion version = HttpVersion.v1, string serviceName = "")
        {
            var controller = method.DeclaringType.Name.RemoveController();

            (HttpMethodType httpType, bool isDefault) = method.GetHttpType();
            if (isDefault) return new HttpAction(httpType, controller, version, serviceName);

            return new HttpAction(httpType, $"{controller}/{method.Name}");
        }

        private static (HttpMethodType httpType, bool isDefault) GetHttpType(this MethodInfo method)
        {
            var httpDefaultGet = method.GetCustomAttribute(StaticKeys.HttpDefaultGetType, false);
            if (httpDefaultGet != null) return (HttpMethodType.Get, true);

            var httpGet = method.GetCustomAttribute(StaticKeys.HttpGetType, false);
            if (httpGet != null) return (HttpMethodType.Get, false);



            var httpDefaultPost = method.GetCustomAttribute(StaticKeys.HttpDefaultPostType, false);
            if (httpDefaultPost != null) return (HttpMethodType.Post, true);

            var httpPost = method.GetCustomAttribute(StaticKeys.HttpPostType, false);
            if (httpPost != null) return (HttpMethodType.Post, false);



            var httpDefaultPut = method.GetCustomAttribute(StaticKeys.HttpDefaultPutType, false);
            if (httpDefaultPut != null) return (HttpMethodType.Put, true);

            var httpPut = method.GetCustomAttribute(StaticKeys.HttpPutType, false);
            if (httpPut != null) return (HttpMethodType.Put, false);



            var httpDefaultPatch = method.GetCustomAttribute(StaticKeys.HttpDefaultPatchType, false);
            if (httpDefaultPatch != null) return (HttpMethodType.Patch, true);

            var httpPatch = method.GetCustomAttribute(StaticKeys.HttpPatchType, false);
            if (httpPatch != null) return (HttpMethodType.Patch, false);



            var httpDefaultDelete = method.GetCustomAttribute(StaticKeys.HttpDefaultDeleteType, false);
            if (httpDefaultDelete != null) return (HttpMethodType.Delete, true);

            var httpDelete = method.GetCustomAttribute(StaticKeys.HttpDeleteType, false);
            if (httpDelete != null) return (HttpMethodType.Delete, false);

            throw new ArgumentException($"{method.Name} not exist http method the attribute");
        }

        public static FieldType GetFieldType(this Type propertyType)
        {
            propertyType = propertyType.GetReadProperty();

            if (propertyType == StaticKeys.IntType || propertyType == StaticKeys.IntNullableType) return FieldType.Integer;
            if (propertyType == StaticKeys.DecimalType || propertyType == StaticKeys.DecimalNullableType ||
                    propertyType == StaticKeys.DoubleType || propertyType == StaticKeys.DoubleNullableType ||
                    propertyType == StaticKeys.FloatType || propertyType == StaticKeys.FloatNullableType) return FieldType.Decimal;

            if (propertyType == StaticKeys.DateTimeType || propertyType == StaticKeys.DateTimeNullableType) return FieldType.DateTime;
            if (propertyType.IsEnum) return FieldType.Enum;
            if (propertyType == StaticKeys.BooleanType || propertyType == StaticKeys.BooleanNullableType) return FieldType.Boolean;

            return FieldType.String;
        }

        public static IEnumerable<ControlBase> BuildControls(this Type formType, DocumentHandler documentHandler)
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

                var control = property.BuildControl(fieldType, documentHandler);
                controls.Add(control);
            }

            var rangeControls = rangeControlCollection.BuildRangePickerControls(documentHandler);
            if (rangeControls.NotNullAndEmpty()) controls.AddRange(rangeControls);

            return controls;
        }

        public static ControlBase BuildControl(this PropertyInfo property, DocumentHandler documentHandler)
        {
            var fieldType = GetFieldType(property.PropertyType);
            return property.BuildControl(fieldType, documentHandler);
        }

        private static ControlBase BuildControl(this PropertyInfo property, FieldType fieldType, DocumentHandler documentHandler)
        {
            if (fieldType == FieldType.Enum)
            {
                var switchAttr = property.GetCustomAttribute<SwitchAttribute>();
                if (switchAttr != null) return new SwitchControl(switchAttr, property, documentHandler);

                return new SelectControl(property, documentHandler);
            }

            var selectAttr = property.GetCustomAttribute<SelectAttribute>();
            if (selectAttr != null) return new SelectControl(selectAttr, property, documentHandler);

            if (fieldType == FieldType.Boolean) return new SwitchControl(property, documentHandler);
            if (fieldType == FieldType.DateTime) return new DatePickerControl(property, documentHandler);

            if (fieldType == FieldType.Decimal || fieldType == FieldType.Integer) return new InputNumberControl(property, documentHandler);

            var textArea = property.GetCustomAttribute<TextAreaAttribute>();
            if (textArea != null) return new TextAreaControl(textArea, property, documentHandler);

            var uploadAttr = property.GetCustomAttribute<UploadAttribute>();
            if (uploadAttr != null) return new UploadControl(uploadAttr, property, documentHandler);

            var passwordAttr = property.GetCustomAttribute<PasswordAttribute>();
            if (passwordAttr != null) return new PasswordControl(passwordAttr, property, documentHandler);

            return new InputControl(property, documentHandler);
        }

        public static IEnumerable<TextValueOption> GetValueOptions(this Type enumType, DocumentHandler documentHandler)
        {
            enumType = enumType.GetReadProperty();

            if (enumType.IsEnum == false) return TextValueOption.Empty();

            var options = new Collection<TextValueOption>();
            var fields = enumType.GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType.IsEnum == false) continue;

                var text = documentHandler.GetFieldSummary(field);
                var textValueOption = new TextValueOption(field.Name, text);
                options.Add(textValueOption);
            }

            return options;
        }

        public static Type GetReadProperty(this PropertyInfo property) => property.PropertyType.GetReadProperty();

        private static Type GetReadProperty(this Type propertyType)
        {
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == StaticKeys.NullableType)
            {
                var genericArguments = propertyType.GetGenericArguments();
                if (genericArguments.NotNullAndEmpty()) return propertyType.GetGenericArguments()[0];
            }

            return propertyType;
        }

        public static string RemoveController(this string controller) => controller.Replace(StaticKeys.ControllerKey, "");

        public static IEnumerable<FieldBase> BuildFields(this Type dataType, DocumentHandler documentHandler)
        {
            return dataType.GetProperties().Select(a => a.BuildField(documentHandler));
        }

        public static IEnumerable<ParamField> BuildParamFields(this Type dataType, DocumentHandler documentHandler)
        {
            return dataType.GetProperties().Select(property => new ParamField(property, documentHandler));
        }

        public static FieldBase BuildField(this PropertyInfo property, DocumentHandler documentHandler)
        {
            var fieldType = property.PropertyType.GetFieldType();
            if (fieldType == FieldType.Enum) return new EnumField(property, documentHandler);

            return new Field(fieldType, property, documentHandler);
        }
    }
}
