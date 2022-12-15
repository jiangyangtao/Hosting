
namespace Yangtao.Hosting.Abstractions.IResult
{
    public interface IResponseResult: IStatusResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        object Data { get; }

        /// <summary>
        /// 数据量
        /// </summary>
        long Count { get; }
    }
}
