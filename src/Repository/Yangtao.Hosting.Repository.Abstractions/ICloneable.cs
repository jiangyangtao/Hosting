
namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface ICloneable<out TEntityBase>
    {
        TEntityBase Clone();
    }

    public interface ICloneableEntity<out TEntityBase> : ICloneable<TEntityBase>
    {

    }

    public interface ICloneableView<out TView> : ICloneable<TView> where TView : IView
    {

    }
}
