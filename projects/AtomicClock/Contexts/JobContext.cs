// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobContext.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the JobContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Contexts
{
    using System.Threading;

    using AtomicClock.Asserts;
    using AtomicClock.Schedulers;

    /// <summary>
    /// The job context.
    /// </summary>
    public class JobContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobContext"/> class.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <param name="jobScheduler">
        /// The job scheduler.
        /// </param>
        public JobContext(CancellationToken cancellationToken, IJobScheduler jobScheduler)
        {
            ArgumentAssert.NotNull(nameof(jobScheduler), jobScheduler);
            ArgumentAssert.NotNull(nameof(cancellationToken), cancellationToken);
            ArgumentAssert.NotCanceled(nameof(cancellationToken), cancellationToken);

            this.CancellationToken = cancellationToken;
            this.JobScheduler = jobScheduler;
        }

        /// <summary>
        /// Gets the cancellation token.
        /// </summary>
        public CancellationToken CancellationToken { get; private set; }

        /// <summary>
        /// Gets the job scheduler.
        /// </summary>
        public IJobScheduler JobScheduler { get; private set; }
    }
}
