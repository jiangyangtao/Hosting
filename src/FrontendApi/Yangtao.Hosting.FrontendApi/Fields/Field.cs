﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Components;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Fields
{
    internal class Field : FieldBase
    {
        public Field(FieldType fieldType, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            FieldType = fieldType;

            var independentEdit = property.GetCustomAttribute<IndependentEditAttribute>();
            if (independentEdit != null) IndependentEdit = new IndependentEdit(property, independentEdit, documentHandler);
        }

        public IndependentEdit? IndependentEdit { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override FieldType FieldType { get; }
    }
}
