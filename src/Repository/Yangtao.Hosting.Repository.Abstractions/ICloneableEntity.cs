
namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface ICloneableEntity<out TEntity> where TEntity : IEntity
    {
        TEntity Clone();
    }
}
