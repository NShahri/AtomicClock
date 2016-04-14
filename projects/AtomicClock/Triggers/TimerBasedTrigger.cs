// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerBasedTrigger.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
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
    using AtomicClock.QueuingPolicies;
    using AtomicClock.Services;

    /// <summary>
    /// The timer based trigger.
    /// </summary>
    internal class TimerBasedTrigger : ITrigger
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly LoggerService Logger = LogManager.GetCurrentClassLogger();

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
            ArgumentAssert.NotNull(nameof(calcNextScheduleRun), calcNextScheduleRun);

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
            ArgumentAssert.NotCanceled(nameof(context.TriggerCancellationToken), context.TriggerCancellationToken);
            if (this.timer != null)
            {
                throw new InvalidOperationException("This trigger was scheduled and can not re-schedule again.");
            }

            this.InitTimer(jobInfo, context);

            this.RegisterCancellationToken(context.TriggerCancellationToken);

            Logger.Debug($"trigger {this.GetType()} scheduled.");

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
        /// The register cancellation token.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        private void RegisterCancellationToken(CancellationToken token)
        {
            token.Register(
                () =>
                    {
                        lock (this.timer)
                        {
                            Logger.Debug($"trigger {this.GetType()} cancelled by cancelltion token.");
                            this.StopTimer();
                        }
                    });
        }

        /// <summary>
        /// The init timer.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        private void InitTimer(IJobInfo jobInfo, TriggerContext context)
        {
            this.timer = new Timer(this.OnTimerCallback, new { jobInfo, context }, Timeout.Infinite, Timeout.Infinite);
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
                Logger.Debug($"trigger {this.GetType()} will be trigger in {nextRun.TotalSeconds} seconds.");
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
            Logger.Debug($"trigger {this.GetType()} stopped.");
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}