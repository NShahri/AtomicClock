namespace AtomicClock.WinService
{
    using System;
    using System.Collections.Generic;

    using AtomicClock.Schedulers;
    using AtomicClock.Triggers;
    using AtomicClock.WinService.Jobs;

    using NLog;

    internal class SampleServiceControl
    {
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
                this.jobScheduler.ScheduleJob<SampleJob, RunPeriodically>("Job Sample 1", TimeSpan.FromSeconds(2));
                this.jobScheduler.ScheduleJob<RunPeriodically>(SampleJob2.DoJob, "Job Sample 2", TimeSpan.FromSeconds(3));
                this.jobScheduler.ScheduleJob<RunPeriodically>(SampleJob3.DoJob, "Job Sample 3", TimeSpan.FromSeconds(3));
                //this.jobScheduler.ScheduleJob<RunPeriodically>((data) => {
                //    Console.WriteLine("Started: {0} - {1}", data, DateTime.Now);
                //    Thread.Sleep(2000);
                //    Console.WriteLine("Done:    {0} - {1}", data, DateTime.Now);
                //}, "Job Sample 5", TimeSpan.FromSeconds(3));
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
                this.jobScheduler = AtomicClockManager.CreateScheduler(1);
                AtomicClock.Services.LogManager.LogAdapter += OnLogAdapter;
                AtomicClock.Services.MetricManager.MetricAapter += OnMetricAapter;
            }

            catch (Exception ex)
            {
                Logger.Fatal(ex, "Server initialization failed.");
                throw;
            }
        }

        private static void OnMetricAapter(string name, TimeSpan elapsedMilliseconds, IEnumerable<string> tags)
        {
            // TODO: Use Data dog or ...
        }

        private static void OnLogAdapter(AtomicClock.Services.LogLevel logLevel, string loggerName, string message, Exception ex)
        {
            LogManager.GetLogger(loggerName).Log(LogLevel.Debug, ex, message);
        }
    }
}
