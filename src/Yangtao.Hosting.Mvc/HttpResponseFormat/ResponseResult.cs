
namespace Yangtao.Hosting.Mvc.HttpResponseFormat
{
    public class ResponseResult
    {
        /// <summary>
        /// 初始化 <see cref="ResponseResult"/> 类的新实例
        /// </summary>
        public ResponseResult() { }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { set; get; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { set; get; }

        /// <summary>
        /// 数据量
        /// </summary>
        public long Count { set; get; }

        /// <summary>
        /// 错误信息 / 提示
        /// </summary>
        public string Message { set; get; }
    }
}
