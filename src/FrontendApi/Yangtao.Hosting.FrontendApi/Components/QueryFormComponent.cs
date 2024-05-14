﻿using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Controls;

namespace Yangtao.Hosting.FrontendApi.Components
{
    internal class QueryFormComponent : ComponentBase
    {
        protected readonly List<ControlBase> ControlList;

        public QueryFormComponent(Type formType, XmlDocumentHandler xmlHandler) : base(formType, xmlHandler)
        {
            ControlList = new();

            var controls = formType.BuildControls(xmlHandler);
            if (controls.NotNullAndEmpty()) ControlList.AddRange(controls);
        }

        public IEnumerable<ControlBase> Controls => ControlList;
    }
}
