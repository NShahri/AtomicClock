// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TriggerContext.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the TriggerContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Contexts
{
    using System.Threading;

    using AtomicClock.Asserts;
    using AtomicClock.Tasks;

    /// <summary>
    /// The trigger context.
    /// </summary>
    public class TriggerContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerContext"/> class.
        /// </summary>
        /// <param name="triggerCancellationToken">
        /// The trigger cancellation token.
        /// </param>
        /// <param name="taskFactory">
        /// The task factory.
        /// </param>
        internal TriggerContext(CancellationToken triggerCancellationToken, ITaskFactory taskFactory)
        {
            ArgumentAssert.NotNull(nameof(taskFactory), taskFactory);
            ArgumentAssert.NotNull(nameof(triggerCancellationToken), triggerCancellationToken);
            ArgumentAssert.NotCanceled(nameof(triggerCancellationToken), triggerCancellationToken);

            this.TriggerCancellationToken = triggerCancellationToken;
            this.TaskFactory = taskFactory;
        }

        /// <summary>
        /// Gets the trigger cancellation token.
        /// </summary>
        public CancellationToken TriggerCancellationToken { get; private set; }

        /// <summary>
        /// Gets the task factory.
        /// </summary>
        public ITaskFactory TaskFactory { get; private set; }
    }
}
