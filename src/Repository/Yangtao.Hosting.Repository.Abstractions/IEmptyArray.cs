
namespace Yangtao.Hosting.Repository.Abstractions
{
    public interface IEmptyArray<out TModel> where TModel : IModel
    {
        TModel[] EmptyArray { get; }
    }
}
