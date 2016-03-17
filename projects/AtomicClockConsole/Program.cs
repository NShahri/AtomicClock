﻿namespace AtomicClock.WinService
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    using Topshelf;

    internal class Program
    {
        private static void Main()
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();

                x.SetDescription("AtomicClock");
                x.SetDisplayName("AtomicClock");
                x.SetServiceName("AtomicClock");

                x.Service<SampleServiceControl>(s =>
                    {
                        s.ConstructUsing(() => new SampleServiceControl());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
            });

            //using (BlockingCollection<int> bc = new BlockingCollection<int>())
            //{

            //    // Kick off a producer task
            //    Task.Factory.StartNew(() =>
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            bc.Add(i);
            //            Thread.Sleep(5000); // sleep 100 ms between adds
            //        }

            //        // Need to do this to keep foreach below from hanging
            //        bc.CompleteAdding();
            //    });

            //    // Now consume the blocking collection with foreach.
            //    // Use bc.GetConsumingEnumerable() instead of just bc because the
            //    // former will block waiting for completion and the latter will
            //    // simply take a snapshot of the current state of the underlying collection.
            //    foreach (var item in bc.GetConsumingEnumerable())
            //    {
            //        Console.WriteLine(item);
            //    }
            //}
        }
    }
}
