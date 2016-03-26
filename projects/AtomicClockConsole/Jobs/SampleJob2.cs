// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleJob2.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   The sample job 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.WinService.Jobs
{
    using System;
    using System.Threading;

    /// <summary>
    /// The sample job 2.
    /// </summary>
    public class SampleJob2
    {
        /// <summary>
        /// The do job.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public static void DoJob(dynamic data)
        {
            Console.WriteLine("Started: {0} - {1}", data, DateTime.Now);
            Thread.Sleep(5000);
            Console.WriteLine("Done:    {0} - {1}", data, DateTime.Now);
        }
    }
}
