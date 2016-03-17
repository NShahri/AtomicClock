namespace AtomicClock.Triggers
{
    using System;
    using System.Threading;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.QueueingPolicies;

    internal class TimerBasedTrigger : ITrigger
    {
        private Timer timer;

        private readonly Func<bool, TimeSpan> calcNextScheduleRun;

        public TimerBasedTrigger(Func<bool, TimeSpan> calcNextScheduleRun)
        {
            this.calcNextScheduleRun = calcNextScheduleRun;
        }

        public void Schedule(JobInfo jobInfo, TriggerContext context)
        {
            if (jobInfo == null)
            {
                throw new ArgumentNullException(nameof(jobInfo));
            }

            this.timer = new Timer(this.OnTimerCallback, new { jobInfo, context }, Timeout.Infinite, Timeout.Infinite);

            context.CancellationToken.Register(() =>
            {
                lock (this.timer)
                {
                    this.StopTimer();
                }
            });

            this.ChangeTimer(this.calcNextScheduleRun(true), context);
        }

        private void OnTimerCallback(dynamic state)
        {
            lock(this.timer)
            {
                this.StopTimer();

                var context = (TriggerContext)state.context;
                var jobInfo = (JobInfo)state.jobInfo;
                if (jobInfo.CheckAllQueueingPolicies(context))
                {
                    // var task = 
                    context.TaskFactory.StartNew(jobInfo);
                    // task.Wait();
                }

                this.ChangeTimer(this.calcNextScheduleRun(false), state.context);
            }
        }

        private void ChangeTimer(TimeSpan nextRun, TriggerContext context)
        {
            if (!context.CancellationToken.IsCancellationRequested)
            {
                this.timer.Change((long)nextRun.TotalMilliseconds, Timeout.Infinite);
            }
            else
            {
                this.StopTimer();
            }
        }

        private void StopTimer()
        {
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
