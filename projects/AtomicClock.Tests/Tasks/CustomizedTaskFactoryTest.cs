// <copyright file="CustomizedTaskFactoryTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>

namespace AtomicClock.Tests.Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AtomicClock.CancellationTokens;
    using AtomicClock.Jobs;
    using AtomicClock.Schedulers;
    using AtomicClock.Tasks;

    using NSubstitute;

    using Xunit;

    /// <summary>
    /// Tests for CustomizedTaskFactory
    /// </summary>
    public class CustomizedTaskFactoryTest
    {
        /// <summary>
        /// Nulls the job scheduler test.
        /// </summary>
        [Fact]
        public void NullJobSchedulerTest()
        {
            var tokenManager = Substitute.For<ICancellationTokensManager>();
            var taskScheduler = new NoneTaskScheduler();

            Assert.Throws<ArgumentNullException>(() => new CustomizedTaskFactory(null, taskScheduler, tokenManager));
        }

        /// <summary>
        /// Nulls the task scheduler test.
        /// </summary>
        [Fact]
        public void NullTaskSchedulerTest()
        {
            var tokenManager = Substitute.For<ICancellationTokensManager>();
            var jobScheduler = Substitute.For<IJobScheduler>();

            Assert.Throws<ArgumentNullException>(() => new CustomizedTaskFactory(jobScheduler, null, tokenManager));
        }

        /// <summary>
        /// Nulls the token manager test.
        /// </summary>
        [Fact]
        public void NullTokenManagerTest()
        {
            var taskScheduler = new NoneTaskScheduler();
            var jobScheduler = Substitute.For<IJobScheduler>();

            Assert.Throws<ArgumentNullException>(() => new CustomizedTaskFactory(jobScheduler, taskScheduler, null));
        }

        /// <summary>
        /// Starts the null job information test.
        /// </summary>
        [Fact]
        public void StartNullJobInfoTest()
        {
            var taskFactory = CreateTaskFactory(tokenManager: null);

            Assert.Throws<ArgumentNullException>(() => { taskFactory.StartNew(null); });
        }

        /// <summary>
        /// Registers the cancellation token test.
        /// </summary>
        [Fact]
        public void RegisterCancellationTokenTest()
        {
            var tokenManager = Substitute.For<ICancellationTokensManager>();
            var taskFactory = CreateTaskFactory(tokenManager: tokenManager);
            var jobInfo = Substitute.For<IJobInfo>();
            taskFactory.StartNew(jobInfo);

            tokenManager.Received().RegisterCancellationToken();
        }

        /// <summary>
        /// Unregisters the cancellation token test.
        /// </summary>
        [Fact]
        public void UnregisterCancellationTokenTest()
        {
            var tokenManager = Substitute.For<ICancellationTokensManager>();
            var taskFactory = CreateTaskFactory(taskScheduler: new SyncTaskScheduler(), tokenManager: tokenManager);
            var jobInfo = Substitute.For<IJobInfo>();
            taskFactory.StartNew(jobInfo);

            tokenManager.Received().UnregisterCancellationToken(CancellationToken.None);
        }

        /// <summary>
        /// Cancelleds the token test.
        /// </summary>
        [Fact]
        public void CancelledTokenTest()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var taskFactory = CreateTaskFactory(token: cancellationTokenSource.Token);
            cancellationTokenSource.Cancel();

            // NO Exception
            var jobInfo = Substitute.For<IJobInfo>();
            taskFactory.StartNew(jobInfo);
        }

        /// <summary>
        /// Creates the task factory.
        /// </summary>
        /// <param name="taskScheduler">The task scheduler.</param>
        /// <param name="jobScheduler">The job scheduler.</param>
        /// <param name="token">The token.</param>
        /// <returns>returns the instance of CustomizedTaskFactory</returns>
        private static ITaskFactory CreateTaskFactory(TaskScheduler taskScheduler = null, IJobScheduler jobScheduler = null, CancellationToken? token = null)
        {
            var tokenManager = Substitute.For<ICancellationTokensManager>();
            tokenManager.RegisterCancellationToken().Returns(token ?? new CancellationTokenSource().Token);
            var taskFactory = CreateTaskFactory(taskScheduler, jobScheduler, tokenManager);

            return taskFactory;
        }

        /// <summary>
        /// Creates the task factory.
        /// </summary>
        /// <param name="taskScheduler">The task scheduler.</param>
        /// <param name="jobScheduler">The job scheduler.</param>
        /// <param name="tokenManager">The token manager.</param>
        /// <returns>returns the instance of CustomizedTaskFactory</returns>
        private static ITaskFactory CreateTaskFactory(TaskScheduler taskScheduler = null, IJobScheduler jobScheduler = null, ICancellationTokensManager tokenManager = null)
        {
            var taskFactory = new CustomizedTaskFactory(jobScheduler ?? Substitute.For<IJobScheduler>(), taskScheduler ?? new NoneTaskScheduler(), tokenManager ?? Substitute.For<ICancellationTokensManager>());

            return taskFactory;
        }
    }
}
