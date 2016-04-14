// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleJob.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   The sample job.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.WinService.Jobs
{
    using System;
    using System.Threading;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;

    /// <summary>
    /// The sample job.
    /// </summary>
    public class SampleJob : IJob
    {
        /// <summary>
        /// The data.
        /// </summary>
        private readonly string data;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleJob"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public SampleJob(string data)
        {
            this.data = data;
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void Execute(JobContext context)
        {
            Console.WriteLine("Started: {0} - {1}", this.data, DateTime.Now);
            Thread.Sleep(5000);
            Console.WriteLine("Done:    {0} - {1}", this.data, DateTime.Now);
        }
    }
}
