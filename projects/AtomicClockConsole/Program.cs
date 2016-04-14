// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.WinService
{
    using Topshelf;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
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
        }
    }
}
