﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks.Dataflow;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.MessageQueue.BufferQueue.Abstracts;

namespace Yangtao.Hosting.MessageQueue.BufferQueue
{
    internal class BufferQueueProvider : IHostedService, IBufferQueueProvider
    {
        private readonly ICollection<IBufferQueue> _queueCollection = new List<IBufferQueue>();

        private volatile bool _stopping = false;

        public IServiceProvider ServiceProvider { get; }

        private readonly ILoggerFactory loggerFactory;

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            _stopping = true;
            return Task.WhenAll(_queueCollection.Select(item => item.StopAsync()));
        }

        public BufferQueueProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            loggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();
        }


        public IBufferQueue<T> CreateBufferQueue<T>(string name, Action<T> handler, int concurrency = 1, int bound = -1)
        {
            if (_stopping)
                throw new InvalidOperationException("application is stopping.");
            var logger = CreateLogger<T>(name);
            var queue = new BufferQueue<T>(this, name, CreateHandler(logger, handler), concurrency, bound);

            _queueCollection.Add(queue);

            return queue;
        }

        public IBufferQueue<T> CreateBufferQueue<T>(string name, Func<T, Task> handler, int concurrency = 1, int bound = -1)
        {
            if (_stopping)
                throw new InvalidOperationException("application is stopping.");

            var logger = CreateLogger<T>(name);
            var queue = new BufferQueue<T>(this, name, CreateHandler(logger, handler), concurrency, bound);

            _queueCollection.Add(queue);

            return queue;
        }

        public IBufferQueue<T> CreateBufferQueue<T>(string name, int batchSize, Action<T[]> handler, int concurrency = 1, int bound = -1)
        {
            if (_stopping)
                throw new InvalidOperationException("application is stopping.");
            var logger = CreateLogger<T>(name);
            var queue = new BatchBufferQueue<T>(this, name, batchSize, CreateHandler(logger, handler), concurrency, bound);

            _queueCollection.Add(queue);

            return queue;
        }

        public IBufferQueue<T> CreateBufferQueue<T>(string name, int batchSize, Func<T[], Task> handler, int concurrency = 1, int bound = -1)
        {
            if (_stopping)
                throw new InvalidOperationException("application is stopping.");
            var logger = CreateLogger<T>(name);
            var queue = new BatchBufferQueue<T>(this, name, batchSize, CreateHandler(logger, handler), concurrency, bound);

            _queueCollection.Add(queue);

            return queue;
        }

        private ILogger CreateLogger<T>(string name)
        {
            return ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(BufferQueue<T>).GetDisplayName() + $"[{name}]");
        }

        protected Func<T, Task> CreateHandler<T>(ILogger logger, Action<T> handler)
        {
            return async item =>
            {
                await Task.Run(() =>
                {
                    try
                    {
                        handler(item);
                    }
                    catch (Exception e)
                    {
                        logger.LogError($"handle message failed.\n{e}");
                    }
                }
                );
            };

        }
        protected Func<T, Task> CreateHandler<T>(ILogger logger, Func<T, Task> handler)
        {
            return async item =>
            {
                await Task.Run(async () =>
                {
                    try
                    {
                        await handler(item);
                    }
                    catch (Exception e)
                    {
                        logger.LogError($"handle message failed.\n{e}");
                    }
                }
                );
            };

        }



        private abstract class BufferQueueBase<T> : IBufferQueue<T>
        {

            public BufferQueueBase(BufferQueueProvider provider, string name, int maxCuncurrency, int capacity = -1)
            {
                Provider = provider;
                Name = name;
                MaxCuncurrency = maxCuncurrency;
                Capacity = capacity;
            }

            public BufferQueueProvider Provider { get; }

            public int MaxCuncurrency { get; }

            public string Name { get; }

            public int Capacity { get; }

            public abstract int Count { get; }

            public abstract Task<bool> PutAsync(T item, CancellationToken cancellationToken);

            public abstract Task StopAsync();


        }






        /// <summary>
        /// 实现一个缓冲队列
        /// </summary>
        /// <typeparam name="T">队列对象类型</typeparam>
        private sealed class BufferQueue<T> : BufferQueueBase<T>
        {


            private readonly ActionBlock<T> _block;

            public override int Count => _block.InputCount;


            /// <summary>
            /// 创建 BufferQueue 实例
            /// </summary>
            /// <param name="provider">创建这个缓冲队列的缓冲队列提供程序</param>
            /// <param name="name"></param>
            /// <param name="handler">队列处理程序</param>
            /// <param name="maxConcurrency">最大并发</param>
            /// <param name="capacity">队列最大容量，默认为无限制</param>
            internal BufferQueue(BufferQueueProvider provider, string name, Func<T, Task> handler, int maxConcurrency, int capacity) : base(provider, name, maxConcurrency, capacity)
            {
                _block = new ActionBlock<T>(handler, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxConcurrency, BoundedCapacity = capacity });
            }



            /// <summary>
            /// 往缓冲队列中放入一个对象
            /// </summary>
            /// <param name="item">要放入的对象</param>
            /// <param name="cancellationToken">取消标识</param>
            /// <returns>是否成功推送到缓冲队列</returns>
            public override Task<bool> PutAsync(T item, CancellationToken cancellationToken)
            {
                return Task.Run(() =>
                {
                    return _block.SendAsync(item, cancellationToken);
                });
            }


            /// <summary>
            /// 在停止之前进行清理操作，等待所有任务处理完成
            /// </summary>
            /// <returns></returns>
            public override Task StopAsync()
            {
                _block.Complete();
                return _block.Completion;
            }
        }

        private sealed class BatchBufferQueue<T> : BufferQueueBase<T>
        {
            private readonly BatchBlock<T> batchBlock;
            private readonly ActionBlock<T[]> actionBlock;

            public BatchBufferQueue(BufferQueueProvider provider, string name, int batchSize, Func<T[], Task> handler, int maxConcurrency, int capacity = -1)
              : base(provider, name, maxConcurrency, capacity)
            {
                batchBlock = new BatchBlock<T>(batchSize, new GroupingDataflowBlockOptions { BoundedCapacity = capacity });
                actionBlock = new ActionBlock<T[]>(handler, new ExecutionDataflowBlockOptions { BoundedCapacity = maxConcurrency, MaxDegreeOfParallelism = maxConcurrency });

                batchBlock.LinkTo(actionBlock);
            }

            public override int Count => batchBlock.OutputCount;

            public override Task<bool> PutAsync(T item, CancellationToken cancellationToken)
            {
                return batchBlock.SendAsync(item, cancellationToken);
            }

            public override async Task StopAsync()
            {
                batchBlock.Complete();
                await batchBlock.Completion;
            }
        }
    }
}
