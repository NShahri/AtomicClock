// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionJobOptionsValidator.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the ActionJobOptionsValidator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Jobs
{
    using AtomicClock.Asserts;

    /// <summary>
    /// The action job options validator.
    /// </summary>
    internal static class ActionJobOptionsValidator
    {
        /// <summary>
        /// The validate and throw.
        /// </summary>
        /// <param name="jobOptions">
        /// The job options.
        /// </param>
        public static void ValidateAndThrow(this ActionJobOptions jobOptions)
        {
            ArgumentAssert.NotNull(nameof(jobOptions), jobOptions);
            ArgumentAssert.NotNull(nameof(jobOptions.Action), jobOptions.Action);
        }
    }
}
