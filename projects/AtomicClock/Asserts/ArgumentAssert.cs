// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentAssert.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the ArgumentAssert type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.Asserts
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    /// <summary>
    /// The assert.
    /// </summary>
    internal static class ArgumentAssert
    {
        /// <summary>
        /// The assert not null.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="o">
        /// The o.
        /// </param>
        [DebuggerNonUserCode]
        public static void NotNull(string name, object o)
        {
            if (o == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// The not canceled.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        public static void NotCanceled(string name, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                throw new OperationCanceledException($"The {name} token has had cancellation requested, and can not be used.", token);
            }
        }
    }
}
