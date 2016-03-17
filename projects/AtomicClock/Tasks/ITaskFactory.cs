namespace AtomicClock.Tasks
{
    using System.Threading.Tasks;

    using AtomicClock.Jobs;

    public interface ITaskFactory
    {
        Task StartNew(JobInfo jobInfo);
    }
}
