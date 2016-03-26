// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RunEveryDayAt.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the RunEveryDayAt type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Triggers
{
    using System;

    using AtomicClock.Asserts;
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;

    /// <summary>
    /// The run every day at.
    /// </summary>
    public class RunEveryDayAt : ITrigger
    {
        /// <summary>
        /// The run time in UTC.
        /// </summary>
        private readonly DateTime runTimeUtc;

        /// <summary>
        /// The now function.
        /// </summary>
        private readonly Func<DateTime> nowFunc;

        /// <summary>
        /// The timer based trigger.
        /// </summary>
        private readonly TimerBasedTrigger timerBasedTrigger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RunEveryDayAt"/> class.
        /// </summary>
        /// <param name="runTimeUtc">
        /// The run time in UTC.
        /// </param>
        public RunEveryDayAt(DateTime runTimeUtc) :
            this(runTimeUtc, () => DateTime.Now)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RunEveryDayAt"/> class.
        /// </summary>
        /// <param name="runTimeUtc">
        /// The run time in UTC.
        /// </param>
        /// <param name="nowFunc">
        /// The now function.
        /// </param>
        internal RunEveryDayAt(DateTime runTimeUtc, Func<DateTime> nowFunc)
        {
            ArgumentAssert.NotNull(nameof(nowFunc), nowFunc);
            ArgumentAssert.NotNull(nameof(runTimeUtc), runTimeUtc);
            if (runTimeUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentOutOfRangeException(nameof(runTimeUtc), @"the parameter has to be UTC based.");
            }

            this.runTimeUtc = runTimeUtc;
            this.nowFunc = nowFunc;
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
        /// The calculation of next schedule run.
        /// </summary>
        /// <param name="firstRun">
        /// The first run.
        /// </param>
        /// <returns>
        /// The <see cref="TimeSpan"/>.
        /// </returns>
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
