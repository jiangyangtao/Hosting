using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Cache.Abstractions
{
    public interface ICacheContainer
    {
        /// <summary>
        /// 容器名
        /// </summary>
        string Name { get; }
    }
}
