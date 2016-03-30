// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionJobOptions.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the ActionJobOptions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Jobs
{
    using System;

    using AtomicClock.Contexts;

    /// <summary>
    /// The action job options.
    /// </summary>
    internal class ActionJobOptions
    {
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public Action<dynamic, JobContext> Action { get; set; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        public dynamic Options { get; set; }
    }
}