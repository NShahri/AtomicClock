// <copyright file="TriggerContextTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>

namespace AtomicClock.Tests.Contexts
{
    using System;
    using System.Threading;

    using AtomicClock.Contexts;
    using AtomicClock.Tasks;

    using NSubstitute;

    using Xunit;

    /// <summary>
    /// Tests for TriggerContext
    /// </summary>
    public class TriggerContextTest
    {
        /// <summary>
        /// Nulls the task factory test.
        /// </summary>
        [Fact]
        public void NullTaskFactoryTest()
        {
            Assert.Throws<ArgumentNullException>(() => new TriggerContext(CancellationToken.None, null));
        }

        /// <summary>
        /// Cancelleds the token test.
        /// </summary>
        [Fact]
        public void CancelledTokenTest()
        {
            var taskFactory = Substitute.For<ITaskFactory>();
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            Assert.Throws<OperationCanceledException>(() => new TriggerContext(cancellationTokenSource.Token, taskFactory));
        }
    }
}
