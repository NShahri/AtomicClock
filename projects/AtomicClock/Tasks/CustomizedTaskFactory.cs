namespace AtomicClock.Tasks
{
    using System.Threading;
    using System.Threading.Tasks;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Schedulers;

    internal class CustomizedTaskFactory : ITaskFactory
    {
        private readonly JobContext jobContext;

        private readonly TaskScheduler taskScheduler;

        public CustomizedTaskFactory(IJobScheduler jobScheduler, TaskScheduler taskScheduler, CancellationToken cancellationToken)
        {
            this.taskScheduler = taskScheduler;
            this.jobContext = new JobContext(cancellationToken, jobScheduler);
        }

        public Task StartNew(JobInfo jobInfo)
        {
            var task = new CustomizedTask(jobInfo, this.jobContext);
            task.Start(this.taskScheduler);
            
            return task;
        }
    }
}
