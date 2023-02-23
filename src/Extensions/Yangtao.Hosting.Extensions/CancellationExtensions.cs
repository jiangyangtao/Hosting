using System.Threading;

namespace Yangtao.Hosting.Extensions
{
    public static class CancellationExtensions
    {
        /// <summary>
        /// 取消并且销毁 <see cref="CancellationTokenSource"/> 对象
        /// </summary>
        /// <param name="source"></param>
        public static void CancelAndDispose(this CancellationTokenSource source)
        {
            try
            {
                source.Cancel();
            }
            finally
            {
                source.Dispose();
            }
        }
    }
}
