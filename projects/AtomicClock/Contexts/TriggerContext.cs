namespace AtomicClock.Contexts
{
    using System.Threading;

    using AtomicClock.Tasks;

    public class TriggerContext
    {
        internal TriggerContext(CancellationToken cancellationToken, ITaskFactory taskFactory, ITaskPool taskPool)
        {
            this.CancellationToken = cancellationToken;
            this.TaskFactory = taskFactory;
            this.TaskPool = taskPool;
        }

        public CancellationToken CancellationToken { get; private set; }

        public ITaskFactory TaskFactory { get; private set; }

        public ITaskPool TaskPool { get; private set; }
    }
}
