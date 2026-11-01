namespace Test.CustomLogger.Application
{
    public sealed class LogGenerator : BackgroundService
    {
        private readonly ILogger<LogGenerator> _logger;
        private int _counter;

        public LogGenerator(ILogger<LogGenerator> logger) => _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _counter++;
                _logger.LogInformation("心跳日志 #{Count}，时间 {Time}", _counter, DateTime.Now);
                if (_counter % 10 == 0)
                    _logger.LogWarning("模拟警告日志，计数达到 {Count}", _counter);

                if (_counter % 30 == 0)
                    _logger.LogError("模拟异常日志，计数达到 {Count},{Exception}", _counter, new Exception("异常测试"));

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
