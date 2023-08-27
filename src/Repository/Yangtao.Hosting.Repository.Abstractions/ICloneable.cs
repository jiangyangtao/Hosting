
namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface ICloneable<out TEntity>
    {
        TEntity Clone();
    }

    public interface ICloneableEntity<TEntity> : ICloneable<TEntity> where TEntity : IEntity
    {

    }

    public interface ICloneableView<TView> : ICloneable<TView> where TView : IView
    {

    }
}
