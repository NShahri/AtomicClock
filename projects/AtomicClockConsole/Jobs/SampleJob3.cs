// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleJob3.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   The sample job 3.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.WinService.Jobs
{
    using System;

    using AtomicClock.Contexts;
    using AtomicClock.Schedulers;
    using AtomicClock.Triggers;

    /// <summary>
    /// The sample job 3.
    /// </summary>
    public class SampleJob3
    {
        /// <summary>
        /// The do job.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        public static void DoJob(dynamic data, JobContext context)
        {
            Console.WriteLine("Started: {0} - {1}", data, DateTime.Now);
            context.JobScheduler.ScheduleJob<RunNowTrigger>(SampleJob4.DoJob);
            Console.WriteLine("Done:    {0} - {1}", data, DateTime.Now);
        }
    }
}
