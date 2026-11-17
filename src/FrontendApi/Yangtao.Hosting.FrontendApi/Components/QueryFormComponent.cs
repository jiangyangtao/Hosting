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

        public QueryFormComponent(Type formType) : base(formType)
        {
            ControlList = new();

            var controls = formType.BuildControls();
            if (controls.NotNullAndEmpty()) ControlList.AddRange(controls);
        }

        public IEnumerable<ControlBase> Controls => ControlList;
    }
}
