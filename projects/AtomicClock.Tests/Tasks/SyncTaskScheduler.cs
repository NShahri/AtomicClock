namespace AtomicClock.Tests.Tasks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class SyncTaskScheduler : TaskScheduler
    {
        protected override void QueueTask(Task task)
        {
            this.TryExecuteTask(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            throw new System.NotImplementedException();
        }
    }
}
