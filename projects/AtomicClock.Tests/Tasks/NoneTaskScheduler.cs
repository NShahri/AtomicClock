// <copyright file="NoneTaskScheduler.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>

namespace AtomicClock.Tests.Tasks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// The simple task scheduler which onlu queues tasks and never runs them
    /// only for testing porpuses
    /// </summary>
    /// <seealso cref="System.Threading.Tasks.TaskScheduler" />
    internal class NoneTaskScheduler : TaskScheduler
    {
        /// <summary>
        /// Queues the task.
        /// </summary>
        /// <param name="task">The task.</param>
        protected override void QueueTask(Task task)
        {
            // DO NOTHING
        }

        /// <summary>
        /// Tries the execute task inline.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="taskWasPreviouslyQueued">if set to <c>true</c> [task was previously queued].</param>
        /// <returns>if result is successful</returns>
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// For debugger support only, generates an enumerable of <see cref="T:System.Threading.Tasks.Task" /> instances currently queued to the scheduler waiting to be executed.
        /// </summary>
        /// <returns>
        /// An enumerable that allows a debugger to traverse the tasks currently queued to this scheduler.
        /// </returns>
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            throw new System.NotImplementedException();
        }
    }
}
