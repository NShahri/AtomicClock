// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerBasedTrigger.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the TimerBasedTrigger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Triggers
{
    using System;
    using System.Threading;

    using AtomicClock.Asserts;
    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.QueueingPolicies;

    /// <summary>
    /// The timer based trigger.
    /// </summary>
    internal class TimerBasedTrigger : ITrigger
    {
        /// <summary>
        /// The calculate next schedule run.
        /// </summary>
        private readonly Func<bool, TimeSpan> calcNextScheduleRun;

        /// <summary>
        /// The timer.
        /// </summary>
        private Timer timer;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TimerBasedTrigger"/> class.
        /// </summary>
        /// <param name="calcNextScheduleRun">
        /// The calculate next schedule run.
        /// </param>
        public TimerBasedTrigger(Func<bool, TimeSpan> calcNextScheduleRun)
        {
            this.calcNextScheduleRun = calcNextScheduleRun;
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

            this.timer = new Timer(this.OnTimerCallback, new { jobInfo, context }, Timeout.Infinite, Timeout.Infinite);

            context.TriggerCancellationToken.Register(() =>
            {
                lock (this.timer)
                {
                    this.StopTimer();
                }
            });

            this.ChangeTimer(this.calcNextScheduleRun(true), context);
        }

        /// <summary>
        /// The on timer callback.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        private void OnTimerCallback(dynamic state)
        {
            lock (this.timer)
            {
                this.StopTimer();

                var context = (TriggerContext)state.context;
                var jobInfo = (IJobInfo)state.jobInfo;
                if (jobInfo.CheckAllQueuingPolicies(context))
                {
                    context.TaskFactory.StartNew(jobInfo);
                }

                this.ChangeTimer(this.calcNextScheduleRun(false), state.context);
            }
        }

        /// <summary>
        /// The change timer.
        /// </summary>
        /// <param name="nextRun">
        /// The next run.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        private void ChangeTimer(TimeSpan nextRun, TriggerContext context)
        {
            if (!context.TriggerCancellationToken.IsCancellationRequested)
            {
                this.timer.Change((long)nextRun.TotalMilliseconds, Timeout.Infinite);
            }
            else
            {
                this.StopTimer();
            }
        }

        /// <summary>
        /// The stop timer.
        /// </summary>
        private void StopTimer()
        {
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
