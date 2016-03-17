namespace AtomicClock.Schedulers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Tasks;
    using AtomicClock.Triggers;

    internal class JobScheduler : IJobScheduler
    {
        private readonly int maximumConcurrencyLevel;

        private readonly List<JobSchedulerInfo> scheduleInfos = new List<JobSchedulerInfo>();

        private readonly List<ITrigger> triggers = new List<ITrigger>();

        private CancellationTokenSource cancelationTokenSource;

        private TriggerContext triggerContext;

        private CustomizedTaskScheduler taskScheduler;

        private bool started;

        public JobScheduler(int maximumConcurrencyLevel)
        {
            this.maximumConcurrencyLevel = maximumConcurrencyLevel;
        }

        public void ScheduleJob<TJob, TTrigger>(dynamic jobOptions = null, dynamic triggerOptions = null)
            where TTrigger : ITrigger where TJob : IJob
        {
            lock (this.scheduleInfos)
            {
                var scheduleInfo = new JobSchedulerInfo(typeof(TTrigger), triggerOptions, typeof(TJob), jobOptions);
                this.scheduleInfos.Add(scheduleInfo);

                if (this.started)
                {
                    this.Start(scheduleInfo);
                }
            }
        }

        public void Shutdown(bool waitForJobsToComplete)
        {
            lock (this.scheduleInfos)
            {
                if (!this.started)
                {
                    return;
                }
                this.started = false;

                this.cancelationTokenSource.Cancel();
                this.taskScheduler.Dispose();
                this.ClearScheduler();
            }
        }

        public void Pause()
        {
            // TODO: not supported
            throw new NotSupportedException();
        }

        public void Start()
        {
            lock (this.scheduleInfos)
            {
                if (this.started)
                {
                    return;
                }
                this.started = true;

                this.InitScheduler();
                foreach (var scheduleInfo in this.scheduleInfos)
                {
                    this.Start(scheduleInfo);
                }
            }
        }

        private void InitScheduler()
        {
            this.cancelationTokenSource = new CancellationTokenSource();
            this.taskScheduler = new CustomizedTaskScheduler(this.maximumConcurrencyLevel);
            var taskFactory = new CustomizedTaskFactory(this, this.taskScheduler, this.cancelationTokenSource.Token);
            this.triggerContext = new TriggerContext(this.cancelationTokenSource.Token, taskFactory, this.taskScheduler);
        }

        private void ClearScheduler()
        {
            this.taskScheduler = null;
            this.triggerContext = null;
            this.triggers.Clear();
            this.scheduleInfos.Clear();
        }

        protected void Start(JobSchedulerInfo jobSchedulerInfo)
        {
            var trigger = jobSchedulerInfo.TriggerInfo.CreateInstance();
            this.triggers.Add(trigger);
            trigger.Schedule(jobSchedulerInfo.JobInfo, this.triggerContext);
        }
    }
}
