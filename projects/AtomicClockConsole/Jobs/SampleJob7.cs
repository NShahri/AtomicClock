// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleJob7.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the SampleJob7 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.WinService.Jobs
{
    using System;

    using AtomicClock.Contexts;

    /// <summary>
    /// The sample job 7.
    /// </summary>
    internal class SampleJob7
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
            context.JobScheduler.DeleteJob(jobInfo => true);
            context.JobScheduler.UnscheduleJob((triggerInfo, jobInfo) => true);
            Console.WriteLine("Done:    {0} - {1}", data, DateTime.Now);
        }
    }
}
