namespace AtomicClock.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AtomicClock.Jobs;

    internal class CustomizedTaskScheduler : TaskScheduler, ITaskPool, IDisposable
    {
        private readonly List<Task> tasks = new List<Task>();

        private readonly List<Task> runningTasks = new List<Task>();

        private int delegatesQueuedOrRunning;

        [ThreadStatic]
        private static bool currentThreadIsProcessingItems;

        public CustomizedTaskScheduler(int maximumConcurrencyLevel)
        {
            if (maximumConcurrencyLevel < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumConcurrencyLevel), "must be greater than zero");    
            }

            this.MaximumConcurrencyLevel = maximumConcurrencyLevel;
        }

        public override int MaximumConcurrencyLevel { get; }

        public IEnumerable<JobInfo> GetTasks()
        {
            lock (this.tasks)
            {
                return this.tasks.Select(f => f).Union(this.runningTasks).Select(f => ((CustomizedTask)f).JobInfo);
            }
        }

        protected override void QueueTask(Task task)
        {
            lock (this.tasks)
            {
                this.tasks.Add(task);
                if (this.delegatesQueuedOrRunning >= this.MaximumConcurrencyLevel)
                {
                    return;
                }

                ++ this.delegatesQueuedOrRunning;
                this.NotifyThreadPoolOfPendingWork();
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            // If this thread isn't already processing a task, we don't support in-lining
            if (!currentThreadIsProcessingItems)
            {
                return false;
            }

            return false;
            //TODO: Support this feature
            //// If the task was previously queued, remove it from the queue
            //if (taskWasPreviouslyQueued)
            //{
            //    this.TryDequeue(task);
            //}
            //return this.TryExecuteTask(task);
        }
        
        protected sealed override bool TryDequeue(Task task)
        {
            lock (this.tasks)
            {
                return this.tasks.Remove(task);
            }
        }
         
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            var lockTaken = false;
            try
            {
                Monitor.TryEnter(this.tasks, ref lockTaken);
                if (lockTaken)
                {
                    return this.tasks;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(this.tasks);
                }
            }
        }

        private void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ =>
            {
                // Note that the current thread is now processing work items.
                // This is necessary to enable in-lining of tasks into this thread.
                currentThreadIsProcessingItems = true;
                try
                {
                    // Process all available items in the queue.
                    while (true)
                    {
                        Task item;
                        lock (this.tasks)
                        {
                            // When there are no more items to be processed,
                            // note that we're done processing, and get out.
                            if (!this.tasks.Any())
                            {
                                --this.delegatesQueuedOrRunning;
                                break;
                            }

                            // Get the next item from the queue
                            item = this.tasks.First();
                            this.tasks.Remove(item);
                            this.runningTasks.Add(item);
                        }

                        // Execute the task we pulled out of the queue
                        this.TryExecuteTask(item);

                        lock (this.tasks)
                        {
                            this.runningTasks.Remove(item);
                        }
                    }
                }
                // We're done processing items on the current thread
                finally
                {
                    currentThreadIsProcessingItems = false;
                }
            }, null);
        }

        public void Dispose()
        {
            lock (this.tasks)
            {
                Task.WaitAll(this.tasks.Union(this.runningTasks).ToArray());
            }
        }
    }
}
