// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RunPeriodically.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the RunPeriodically type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Triggers
{
    using System;
    using System.Linq;

    using AtomicClock.Asserts;
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;

    /// <summary>
    /// The run periodically.
    /// </summary>
    public class RunPeriodically : ITrigger
    {
        /// <summary>
        /// The period.
        /// </summary>
        private readonly TimeSpan period;

        /// <summary>
        /// The first run now.
        /// </summary>
        private readonly bool firstRunNow;

        /// <summary>
        /// The timer based trigger.
        /// </summary>
        private readonly TimerBasedTrigger timerBasedTrigger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RunPeriodically"/> class.
        /// </summary>
        /// <param name="period">
        /// The period.
        /// </param>
        /// <param name="firstRunNow">
        /// The first run now.
        /// </param>
        public RunPeriodically(TimeSpan period, bool firstRunNow = true)
        {
            ArgumentAssert.NotNull(nameof(period), period);

            this.period = period;
            this.firstRunNow = firstRunNow;
            this.timerBasedTrigger = new TimerBasedTrigger(this.CalcNextScheduleRun);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RunPeriodically"/> class.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public RunPeriodically(dynamic options)
        {
            Type o = options.GetType();
            var periodProperty = o.GetProperties().FirstOrDefault(f => f.Name.Equals("period", StringComparison.InvariantCultureIgnoreCase));
            var firstRunNowProperty = o.GetProperties().FirstOrDefault(f => f.Name.Equals("firstRunNow", StringComparison.InvariantCultureIgnoreCase));

            TimeSpan periodValue;
            if (periodProperty != null)
            {
                periodValue = periodProperty.GetValue(options);
            }
            else if (options is TimeSpan)
            {
                periodValue = options;
            }
            else
            { 
                throw new NullReferenceException("period");
            }

            var firstRunNowValue = true;
            if (firstRunNowProperty != null)
            {
                firstRunNowValue = firstRunNowProperty.GetValue(options);
            }

            this.period = periodValue;
            this.firstRunNow = firstRunNowValue;
            this.timerBasedTrigger = new TimerBasedTrigger(this.CalcNextScheduleRun);
        }

        /// <summary>
        /// The schedule.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        public void Schedule(IJobInfo jobInfo, TriggerContext context)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);
            ArgumentAssert.NotNull(nameof(context), context);

            this.timerBasedTrigger.Schedule(jobInfo, context);
        }

        /// <summary>
        /// The calculator for next schedule run.
        /// </summary>
        /// <param name="firstRun">
        /// The first run.
        /// </param>
        /// <returns>
        /// The <see cref="TimeSpan"/>.
        /// </returns>
        protected TimeSpan CalcNextScheduleRun(bool firstRun = false)
        {
            return firstRun ? (this.firstRunNow ? TimeSpan.Zero : this.period) : this.period;
        }
    }
}
