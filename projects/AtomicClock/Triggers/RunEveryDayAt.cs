namespace AtomicClock.Triggers
{
    using System;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Schedulers;

    public class RunEveryDayAt : ITrigger
    {
        private readonly DateTime runTimeUtc;

        private readonly Func<DateTime> nowFunc;

        private readonly TimerBasedTrigger timerBasedTrigger;

        public RunEveryDayAt(DateTime runTimeUtc) :
            this(runTimeUtc, () => DateTime.Now)
        {
        }

        internal RunEveryDayAt(DateTime runTimeUtc, Func<DateTime> nowFunc)
        {
            if (runTimeUtc == null)
            {
                throw new ArgumentNullException(nameof(runTimeUtc));
            }

            if (runTimeUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentOutOfRangeException(nameof(runTimeUtc), @"the parameter has to be UTC based.");
            }

            this.runTimeUtc = runTimeUtc;
            this.nowFunc = nowFunc;
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
            var now = this.nowFunc();
            var nextRunTimeUtc = now.Date.Add(this.runTimeUtc.TimeOfDay);
            if (nextRunTimeUtc < now)
            {
                nextRunTimeUtc = nextRunTimeUtc.AddDays(1);
            }

            return nextRunTimeUtc - now;
        }
    }
}
