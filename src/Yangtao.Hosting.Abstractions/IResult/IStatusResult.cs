
namespace Yangtao.Hosting.Abstractions.IResult
{
    public interface IStatusResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; }
    }
}
