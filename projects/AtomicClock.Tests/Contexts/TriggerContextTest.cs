namespace AtomicClock.Tests.Contexts
{
    using System;
    using System.Threading;

    using AtomicClock.Contexts;
    using AtomicClock.Tasks;

    using NSubstitute;

    using Xunit;

    public class TriggerContextTest
    {
        [Fact]
        public void NullTaskFactoryTest()
        {
            Assert.Throws<ArgumentNullException>(() => new TriggerContext(CancellationToken.None, null));
        }

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
