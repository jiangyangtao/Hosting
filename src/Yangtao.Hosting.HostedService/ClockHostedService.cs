using Microsoft.Extensions.Hosting;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.HostedService
{
    public abstract class ClockHostedService : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly object _sync = new();
        private bool running = false;
        private CancellationTokenSource _source = new CancellationTokenSource();

        protected ClockHostedService(TimeSpan interval)
        {
            Init(interval);
        }

        private void Init(TimeSpan interval)
        {
            _timer = new System.Timers.Timer(interval.TotalMilliseconds);
            _timer.Elapsed += (sender, e) =>
            {
                lock (_sync)
                {
                    _source.CancelAndDispose();
                    _source = new CancellationTokenSource();
                }

                OnClocking();

            };
        }

        /// <summary>
        /// 派生类重写此方法实现定时触发的逻辑
        /// </summary>
        protected abstract void OnClocking();

        /// <summary>
        /// 设置触发时间间隔
        /// </summary>
        /// <param name="interval">时间间隔</param>
        protected void SetInterval(TimeSpan interval)
        {
            if (_timer != null && interval.TotalMilliseconds != _timer.Interval)
            {
                _timer.Dispose();
                _timer = null;
            }

            if (_timer == null)
            {
                Init(interval);
                if (running) _timer.Start();
            }

        }

        void IDisposable.Dispose()
        {
            _timer.Dispose();
            _source.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            running = true;

            if (_timer != null) _timer.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            running = false;

            if (_timer != null)
            {
                _timer.Stop();
                _source.Cancel();
            }


            return Task.CompletedTask;
        }
    }
}