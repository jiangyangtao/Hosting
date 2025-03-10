using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RadioGroupAttribute: Attribute
    {
        public RadioGroupType RadioGroupType { set; get; }
    }
}
