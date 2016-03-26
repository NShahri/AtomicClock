﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobActivator.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the JobActivator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Jobs
{
    using System;

    using AtomicClock.Asserts;

    /// <summary>
    /// The job activator.
    /// </summary>
    internal static class JobActivator
    {
        /// <summary>
        /// The create instance.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        /// <returns>
        /// The <see cref="IJob"/>.
        /// </returns>
        public static IJob CreateInstance(this IJobInfo jobInfo)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);

            return (IJob)Activator.CreateInstance(jobInfo.JobType, jobInfo.JobOptions);
        }
    }
}
