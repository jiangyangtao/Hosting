namespace Yangtao.Hosting.Mvc.Abstractions
{
    public interface IResponseResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        int Code { get; }

        /// <summary>
        /// 数据
        /// </summary>
        object Data { get; }

        /// <summary>
        /// 数据量
        /// </summary>
        long Count { get; }

        /// <summary>
        /// 错误信息 / 提示
        /// </summary>
        string Message { get; }
    }
}
