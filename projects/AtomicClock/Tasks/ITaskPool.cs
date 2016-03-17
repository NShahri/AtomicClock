namespace AtomicClock.Tasks
{
    using System.Collections.Generic;

    using AtomicClock.Jobs;

    public interface ITaskPool
    {
        IEnumerable<JobInfo> GetTasks();
    }
}
