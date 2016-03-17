namespace AtomicClock.Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Services;

    internal class CustomizedTask : Task
    {
        private static readonly LoggerService Logger = LogManager.GetCurrentClassLogger();

        public JobInfo JobInfo { get; private set; }

        public CustomizedTask(JobInfo jobInfo, JobContext jobContext) 
            : base(() => RunJob(jobInfo, jobContext), 
                  jobInfo.ExecuteOnCancellation ? CancellationToken.None : jobContext.CancellationToken)
        {
            this.JobInfo = jobInfo;
        }

        private static void RunJob(JobInfo jobInfo, JobContext jobContext)
        {
            var job = jobInfo.CreateInstance();
            try
            {
                job.Execute(jobContext);
            }
            catch (Exception ex)
            {
                // unexpected exception, IGNORE IT
                Logger.Error("Job execution exception", ex);
            }
        }
    }
}
