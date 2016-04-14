// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobInfoValidator.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the TriggerInfoValidator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Jobs
{
    using System;

    using AtomicClock.Asserts;

    /// <summary>
    /// The job info validation.
    /// </summary>
    internal static class JobInfoValidator
    {
        /// <summary>
        /// The validate and throw.
        /// </summary>
        /// <param name="jobInfo">
        /// The job info.
        /// </param>
        public static void ValidateAndThrow(this IJobInfo jobInfo)
        {
            ArgumentAssert.NotNull(nameof(jobInfo), jobInfo);

            if (!typeof(IJob).IsAssignableFrom(jobInfo.JobType))
            {
                throw new InvalidOperationException("Provided job type has to be based of IJob.");
            }
        }
    }
}
