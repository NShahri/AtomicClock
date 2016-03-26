namespace AtomicClock.Jobs
{
    using System;

    using AtomicClock.Asserts;
    using AtomicClock.Contexts;
    using AtomicClock.Services;

    internal class ActionJob : IJob
    {
        private readonly ActionJobOptions options;

        private static readonly LoggerService Logger = LogManager.GetCurrentClassLogger();

        public ActionJob(ActionJobOptions options)
        {
            ArgumentAssert.NotNull(nameof(options), options);
            options.ValidateAndThrow();

            this.options = options;
        }

        public void Execute(JobContext context)
        {
            var jobName = this.GetType().Name;

            try
            {
                using (MetricManager.StartTimer("job.duration", $"job.name:{jobName}"))
                {
                    this.OnExecute(context);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"An exception has occurred running {jobName}", ex);
                throw;
            }
        }

        protected void OnExecute(JobContext context)
        {
            var jobAction = this.options.Action;
            jobAction(this.options.Options, context);
        }
    }
}
