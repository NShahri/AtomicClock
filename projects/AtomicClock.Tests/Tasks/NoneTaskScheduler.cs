namespace AtomicClock.Tests.Tasks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class NoneTaskScheduler : TaskScheduler
    {
        protected override void QueueTask(Task task)
        {
            // DO NOTHING
        }

        protected override bool TryDequeue(Task task)
        {
            return base.TryDequeue(task);
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
