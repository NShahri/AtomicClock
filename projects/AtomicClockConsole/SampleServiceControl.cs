// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleServiceControl.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the SampleServiceControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.WinService
{
    using System;
    using System.Collections.Generic;

    using AtomicClock.Schedulers;
    using AtomicClock.Triggers;
    using AtomicClock.WinService.Jobs;

    using NLog;

    /// <summary>
    /// The sample service control.
    /// </summary>
    internal class SampleServiceControl
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     The jobScheduler.
        /// </summary>
        private IJobScheduler jobScheduler;

        /// <summary>
        ///     Starts this instance, delegates to jobScheduler.
        /// </summary>
        public virtual void Start()
        {
            try
            {
                LogHelper.NLogToConsole();
                this.InitializeScheduler();
                this.InitializeJobs();
                this.jobScheduler.Start();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "jobScheduler start failed.");
                throw;
            }

            Logger.Info("jobScheduler started successfully");
        }

        /// <summary>
        ///     Stops this instance, delegates to jobScheduler.
        /// </summary>
        public virtual void Stop()
        {
            try
            {
                this.jobScheduler.Shutdown(true);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "jobScheduler stop failed.");
                throw;
            }

            Logger.Info("jobScheduler shutdown complete");
        }

        /// <summary>
        ///     The initialize jobs.
        /// </summary>
        protected void InitializeJobs()
        {
            try
            {
                // define a job as a class
                this.jobScheduler.ScheduleJob<SampleJob, RunPeriodically>("Job Sample 1", TimeSpan.FromSeconds(3));

                // define a job as an action
                this.jobScheduler.ScheduleJob<RunPeriodically>(SampleJob2.DoJob, "Job Sample 2", TimeSpan.FromSeconds(3));

                // scheduling a job inside another job
                this.jobScheduler.ScheduleJob<RunPeriodically>(SampleJob3.DoJob, "Job Sample 3", TimeSpan.FromSeconds(3));

                // pass data to a job
                this.jobScheduler.ScheduleJob<RunPeriodically>(SampleJob5.DoJob, "Job Sample 5", TimeSpan.FromSeconds(3));

                // not handled exceptions happens inside the job
                this.jobScheduler.ScheduleJob<RunPeriodically>(SampleJob6.DoJob, "Job Sample 6", TimeSpan.FromSeconds(3));

                // unscheduled all tasks in 20 seconds
                this.jobScheduler.ScheduleJob<RunPeriodically>(SampleJob7.DoJob, "Job Sample 7", new { period = TimeSpan.FromSeconds(20), firstRunNow = false });
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "jobScheduler start failed");
                throw;
            }

            Logger.Info("jobScheduler started successfully");
        }

        /// <summary>
        ///     The initialize jobScheduler.
        /// </summary>
        protected void InitializeScheduler()
        {
            if (this.jobScheduler != null)
            {
                return;
            }

            try
            {
                this.jobScheduler = AtomicClockManager.CreateScheduler(10);
                AtomicClock.Services.LogManager.LogAdapter += OnLogAdapter;
                AtomicClock.Services.MetricManager.MetricAapter += OnMetricAdapter;
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "Server initialization failed.");
                throw;
            }
        }

        /// <summary>
        /// The on metric adapter.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="elapsedMilliseconds">
        /// The elapsed milliseconds.
        /// </param>
        /// <param name="tags">
        /// The tags.
        /// </param>
        private static void OnMetricAdapter(string name, TimeSpan elapsedMilliseconds, IEnumerable<string> tags)
        {
            // TODO: Use Data dog or ...
        }

        /// <summary>
        /// The on log adapter.
        /// </summary>
        /// <param name="logLevel">
        /// The log level.
        /// </param>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        private static void OnLogAdapter(AtomicClock.Services.LogLevel logLevel, string loggerName, string message, Exception ex)
        {
            LogManager.GetLogger(loggerName).Log(logLevel.ToNLogLevel(), ex, message);
        }
    }
}
