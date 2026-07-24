using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Controls;

namespace Yangtao.Hosting.FrontendApi.Components
{
    /// <summary>
    /// 查询组件
    /// </summary>
    internal class QueryFormComponent : ComponentBase
    {
        protected readonly List<ControlBase> ControlList;

        public QueryFormComponent(Type formType, DocumentHandler documentHandler) : base(formType, documentHandler)
        {
            ControlList = new();

            var controls = formType.BuildControls(documentHandler);
            if (controls.NotNullAndEmpty()) ControlList.AddRange(controls);
        }

        public IEnumerable<ControlBase> Controls => ControlList;
    }
}
