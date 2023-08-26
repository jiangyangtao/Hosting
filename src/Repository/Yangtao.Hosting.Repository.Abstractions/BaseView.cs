
namespace Yangtao.Hosting.Repository.Abstractions
{
    public abstract class BaseView : IView, ICloneableView<IView>
    {
        public IView Clone()
        {
            return (IView)MemberwiseClone();
        }
    }
}
