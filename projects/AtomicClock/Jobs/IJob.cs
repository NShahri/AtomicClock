// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJob.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the IJob type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Jobs
{
    using AtomicClock.Contexts;

    /// <summary>
    /// The Job interface.
    /// </summary>
    public interface IJob
    {
        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        void Execute(JobContext context);
    }
}
