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

    public class CustomizedTaskFactoryTest
    {
        [Fact]
        public void NullJobSchedulerTest()
        {
            var tokenManager = Substitute.For<ICancellationTokensManager>();
            var taskScheduler = new NoneTaskScheduler();

            Assert.Throws<ArgumentNullException>(() => new CustomizedTaskFactory(null, taskScheduler, tokenManager));
        }

        [Fact]
        public void NullTaskSchedulerTest()
        {
            var tokenManager = Substitute.For<ICancellationTokensManager>();
            var jobScheduler = Substitute.For<IJobScheduler>();

            Assert.Throws<ArgumentNullException>(() => new CustomizedTaskFactory(jobScheduler, null, tokenManager));
        }

        [Fact]
        public void NullTokenManagerTest()
        {
            var taskScheduler = new NoneTaskScheduler();
            var jobScheduler = Substitute.For<IJobScheduler>();

            Assert.Throws<ArgumentNullException>(() => new CustomizedTaskFactory(jobScheduler, taskScheduler, null));
        }

        [Fact]
        public void StartNullJobInfoTest()
        {
            var taskFactory = CreateTaskFactory(tokenManager: null);

            Assert.Throws<ArgumentNullException>(() => { taskFactory.StartNew(null); });
        }

        [Fact]
        public void RegisterCancellationTokenTest()
        {
            var tokenManager = Substitute.For<ICancellationTokensManager>();
            var taskFactory = CreateTaskFactory(tokenManager: tokenManager);
            var jobInfo = Substitute.For<IJobInfo>();
            taskFactory.StartNew(jobInfo);

            tokenManager.Received().RegisterCancellationToken();
        }

        [Fact]
        public void UnregisterCancellationTokenTest()
        {
            var tokenManager = Substitute.For<ICancellationTokensManager>();
            var taskFactory = CreateTaskFactory(taskScheduler: new SyncTaskScheduler(), tokenManager: tokenManager);
            var jobInfo = Substitute.For<IJobInfo>();
            taskFactory.StartNew(jobInfo);

            tokenManager.Received().UnregisterCancellationToken(CancellationToken.None);
        }

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

        private static ITaskFactory CreateTaskFactory(TaskScheduler taskScheduler = null, IJobScheduler jobScheduler = null, CancellationToken? token = null)
        {
            var tokenManager = Substitute.For<ICancellationTokensManager>();
            tokenManager.RegisterCancellationToken().Returns(token ?? new CancellationTokenSource().Token);
            var taskFactory = CreateTaskFactory(taskScheduler, jobScheduler, tokenManager);

            return taskFactory;
        }

        private static ITaskFactory CreateTaskFactory(TaskScheduler taskScheduler = null, IJobScheduler jobScheduler = null, ICancellationTokensManager tokenManager = null)
        {
            var taskFactory = new CustomizedTaskFactory(jobScheduler ?? Substitute.For<IJobScheduler>(), taskScheduler ?? new NoneTaskScheduler(), tokenManager ?? Substitute.For<ICancellationTokensManager>());

            return taskFactory;
        }
    }
}
