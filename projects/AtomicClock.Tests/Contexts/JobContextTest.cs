namespace AtomicClock.Tests.Contexts
{
    using System;
    using System.Threading;

    using AtomicClock.Contexts;
    using AtomicClock.Schedulers;

    using NSubstitute;

    using Xunit;

    public class JobContextTest
    {
        [Fact]
        public void NullJobSchedulerTest()
        {
            Assert.Throws<ArgumentNullException>(() => new JobContext(CancellationToken.None, null));
        }

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
