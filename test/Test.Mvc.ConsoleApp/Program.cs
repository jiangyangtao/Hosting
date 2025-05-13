

using System.Xml.Linq;
using System.Xml.XPath;

namespace Test.Mvc.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var memberName = "M:Admin.Employee.Application.Controllers.SystemLogController.ActionLogs";
            var d = Document;
            var r = d.XPathEvaluate($"normalize-space(//member[contains(@name,'{memberName}')]/summary/text())");

            Console.WriteLine(r);
        }


        private static XDocument Document
        {
            get
            {
                return XDocument.Load("E:\\Work\\Open Source\\Hosting\\test\\Test.Mvc.ConsoleApp\\XMLFile.xml");
            }
        }
    }
}
