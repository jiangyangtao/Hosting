using System.Reflection;
using System.Xml.Linq;

namespace Yangtao.Hosting.Mvc.Abstractions
{
    public interface IApplicationDocumentProvider
    {
        internal void InitDocument();

        public XDocument? ApplicationDocument { get; }

        string GetControllerDescription(TypeInfo controllerTypeInfo);

        string GetActionDescription(MethodInfo actionTypeInfo);
    }
}
