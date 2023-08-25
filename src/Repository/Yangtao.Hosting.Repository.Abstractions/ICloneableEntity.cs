using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface ICloneableEntity<out TEntity> where TEntity : IEntity
    {
        TEntity Clone();
    }
}
