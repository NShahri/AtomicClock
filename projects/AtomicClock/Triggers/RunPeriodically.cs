namespace AtomicClock.Triggers
{
    using System;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Schedulers;

    public class RunPeriodically : ITrigger
    {
        private readonly TimeSpan period;

        private readonly TimerBasedTrigger timerBasedTrigger;

        public RunPeriodically(TimeSpan period)
        {
            if (period == null)
            {
                throw new ArgumentNullException(nameof(period));
            }

            this.period = period;
            this.timerBasedTrigger = new TimerBasedTrigger(this.CalcNextScheduleRun);
        }

        public void Schedule(JobInfo jobInfo, TriggerContext context)
        {
            if (jobInfo == null)
            {
                throw new ArgumentNullException(nameof(jobInfo));
            }

            this.timerBasedTrigger.Schedule(jobInfo, context);
        }

        protected TimeSpan CalcNextScheduleRun(bool firstRun = false)
        {
            return firstRun ? TimeSpan.Zero : this.period;
        }
    }
}
