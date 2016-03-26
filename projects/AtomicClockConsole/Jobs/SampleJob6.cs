// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleJob6.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the SampleJob6 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.WinService.Jobs
{
    using System;

    /// <summary>
    /// The sample job 6.
    /// </summary>
    public class SampleJob6
    {
        /// <summary>
        /// The do job.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public static void DoJob(dynamic data)
        {
            Console.WriteLine("Started: {0} - {1}", data, DateTime.Now);
            Console.WriteLine("Done:    {0} - {1}", data, DateTime.Now);
            throw new NotImplementedException("Exception in job");
        }
    }
}
