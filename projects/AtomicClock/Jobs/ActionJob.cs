// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionJob.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the ActionJob type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Jobs
{
    using AtomicClock.Asserts;
    using AtomicClock.Contexts;

    /// <summary>
    /// The action job.
    /// </summary>
    internal class ActionJob : IJob
    {
        /// <summary>
        /// The options.
        /// </summary>
        private readonly ActionJobOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionJob"/> class.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public ActionJob(ActionJobOptions options)
        {
            ArgumentAssert.NotNull(nameof(options), options);
            options.ValidateAndThrow();

            this.options = options;
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void Execute(JobContext context)
        {
            var jobAction = this.options.Action;
            jobAction(this.options.Options, context);
        }
    }
}
