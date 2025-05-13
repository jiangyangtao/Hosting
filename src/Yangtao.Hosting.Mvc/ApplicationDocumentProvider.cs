using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Yangtao.Hosting.Mvc.Abstractions;

namespace Yangtao.Hosting.Mvc
{
    internal class ApplicationDocumentProvider : IApplicationDocumentProvider
    {
        private readonly ILogger<IApplicationDocumentProvider> _logger;

        public ApplicationDocumentProvider(ILogger<IApplicationDocumentProvider> logger)
        {
            _logger = logger;
        }

        public XDocument? ApplicationDocument { private set; get; }

        public string GetControllerDescription(TypeInfo controllerTypeInfo)
        {
            var memberName = $"T:{controllerTypeInfo.FullName}";

            return GetSummaryText(memberName);
        }

        public string GetActionDescription(MethodInfo actionTypeInfo)
        {
            var memberName = $"M:{actionTypeInfo.DeclaringType.FullName}.{actionTypeInfo.Name}";

            return GetSummaryText(memberName);
        }

        private string GetSummaryText(string memberName)
        {
            if (ApplicationDocument == null) return string.Empty;

            var r = ApplicationDocument.XPathEvaluate($"normalize-space(//member[@name = '{memberName}']/summary/text())") as string;
            r ??= ApplicationDocument.XPathEvaluate($"normalize-space(//member[contains(@name,'{memberName}')]/summary/text())") as string;
            return r ?? string.Empty;
        }

        void IApplicationDocumentProvider.InitDocument()
        {
            var r = Assembly.GetEntryAssembly();
            if (r == null)
            {
                _logger.LogError("Not found entry application, please set the launch project.");
                return;
            }

            var xmlFile = $"{r.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            var existPath = File.Exists(xmlPath);
            if (existPath == false)
            {
                _logger.LogError($"Not found {xmlFile} the file, please set the document output of the project.");
                return;
            }

            ApplicationDocument = XDocument.Load(xmlPath);
            _logger.LogInformation($"Successed load document file: {xmlFile}.");
        }
    }
}
