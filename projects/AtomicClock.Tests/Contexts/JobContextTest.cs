// <copyright file="JobContextTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>

namespace AtomicClock.Tests.Contexts
{
    using System;
    using System.Threading;

    using AtomicClock.Contexts;
    using AtomicClock.Schedulers;

    using NSubstitute;

    using Xunit;

    /// <summary>
    /// Tests for JobContext
    /// </summary>
    public class JobContextTest
    {
        /// <summary>
        /// Nulls the job scheduler test.
        /// </summary>
        [Fact]
        public void NullJobSchedulerTest()
        {
            Assert.Throws<ArgumentNullException>(() => new JobContext(CancellationToken.None, null));
        }

        /// <summary>
        /// Cancelleds the token test.
        /// </summary>
        [Fact]
        public void CancelledTokenTest()
        {
            var jobScheduler = Substitute.For<IJobScheduler>();
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            Assert.Throws<OperationCanceledException>(() => new JobContext(cancellationTokenSource.Token, jobScheduler));
        }
    }
}
