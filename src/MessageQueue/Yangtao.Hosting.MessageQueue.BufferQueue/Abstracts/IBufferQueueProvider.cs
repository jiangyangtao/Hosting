
namespace Yangtao.Hosting.MessageQueue.BufferQueue.Abstracts
{
    public interface IBufferQueueProvider
    {
        /// <summary>
        /// 创建缓冲队列
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="name">缓冲队列名称</param>
        /// <param name="handler">元素处理程序</param>
        /// <param name="concurrency">处理并行度</param>
        /// <param name="capacity">最大缓冲大小</param>
        /// <returns></returns>
        IBufferQueue<T> CreateBufferQueue<T>(string name, Action<T> handler, int concurrency = 1, int capacity = -1);

        /// <summary>
        /// 创建缓冲队列
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="name">缓冲队列名称</param>
        /// <param name="handler">元素处理程序</param>
        /// <param name="concurrency">处理并行度</param>
        /// <param name="capacity">最大缓冲大小</param>
        /// <returns></returns>
        IBufferQueue<T> CreateBufferQueue<T>(string name, Func<T, Task> handler, int concurrency = 1, int capacity = -1);


        /// <summary>
        /// 创建缓冲队列
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="name">缓冲队列名称</param>
        /// <param name="batchSize">批次大小</param>
        /// <param name="handler">元素处理程序</param>
        /// <param name="concurrency">处理并行度</param>
        /// <param name="capacity">最大缓冲大小</param>
        /// <returns></returns>
        IBufferQueue<T> CreateBufferQueue<T>(string name, int batchSize, Action<T[]> handler, int concurrency = 1, int capacity = -1);

        /// <summary>
        /// 创建缓冲队列
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="name">缓冲队列名称</param>
        /// <param name="batchSize">批次大小</param>
        /// <param name="handler">元素处理程序</param>
        /// <param name="concurrency">处理并行度</param>
        /// <param name="capacity">最大缓冲大小</param>
        /// <returns></returns>
        IBufferQueue<T> CreateBufferQueue<T>(string name, int batchSize, Func<T[], Task> handler, int concurrency = 1, int capacity = -1);
    }
}
