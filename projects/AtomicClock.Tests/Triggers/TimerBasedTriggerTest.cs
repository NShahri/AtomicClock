// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerBasedTriggerTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the TimerBasedTriggerTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Tests.Triggers
{
    using System;
    using System.Threading;

    using AtomicClock.Contexts;
    using AtomicClock.Jobs;
    using AtomicClock.Tasks;
    using AtomicClock.Triggers;

    using NSubstitute;

    using Xunit;

    /// <summary>
    /// The timer based trigger test.
    /// </summary>
    public class TimerBasedTriggerTest
    {
        /// <summary>
        /// Nulls the function test.
        /// </summary>
        [Fact]
        public void NullFuncTest()
        {
            Assert.Throws<ArgumentNullException>(() => new TimerBasedTrigger(null));
        }

        /// <summary>
        /// Cancelleds the token test.
        /// </summary>
        [Fact]
        public void CancelledTokenTest()
        {
            var taskFactory = Substitute.For<ITaskFactory>();
            var jobInfo = Substitute.For<IJobInfo>();
            var cancellationTokenSource = new CancellationTokenSource();
            var context = new TriggerContext(cancellationTokenSource.Token, taskFactory);
            var trigger = new TimerBasedTrigger((bFirst) => TimeSpan.FromSeconds(1));

            cancellationTokenSource.Cancel();
            Assert.Throws<OperationCanceledException>(() => trigger.Schedule(jobInfo, context));
        }

        /// <summary>
        /// Nulls the job information test.
        /// </summary>
        [Fact]
        public void NullJobInfoTest()
        {
            var taskFactory = Substitute.For<ITaskFactory>();
            var context = new TriggerContext(CancellationToken.None, taskFactory);
            var trigger = new TimerBasedTrigger((bFirst) => TimeSpan.FromSeconds(1));

            Assert.Throws<ArgumentNullException>(() => trigger.Schedule(null, context));
        }

        /// <summary>
        /// Nulls the context test.
        /// </summary>
        [Fact]
        public void NullContextTest()
        {
            var jobInfo = Substitute.For<IJobInfo>();
            var trigger = new TimerBasedTrigger((bFirst) => TimeSpan.FromSeconds(1));

            Assert.Throws<ArgumentNullException>(() => trigger.Schedule(jobInfo, null));
        }
    }
}
