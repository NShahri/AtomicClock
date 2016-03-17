namespace AtomicClock.Jobs
{
    using AtomicClock.Contexts;

    public interface IJob
    {
        void Execute(JobContext context);
    }
}
