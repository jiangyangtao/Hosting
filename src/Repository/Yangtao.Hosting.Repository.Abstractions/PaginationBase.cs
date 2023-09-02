
namespace Yangtao.Hosting.Repository.Abstractions
{
    public abstract class PaginationBase
    {
        public int Start { set; get; } = 0;

        public int Size { set; get; } = 10;
    }
}
