// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkedCancellationTokensesManager.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the SimpleCancellationTokenManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.CancellationTokens
{
    using System.Threading;

    using AtomicClock.Asserts;

    /// <summary>
    /// The collection linked cancellation token manager.
    /// </summary>
    internal class LinkedCancellationTokensesManager : CancellationTokensesManager
    {
        /// <summary>
        /// The scheduler cancellation token.
        /// </summary>
        private readonly CancellationToken schedulerCancellationToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedCancellationTokensesManager"/> class.
        /// </summary>
        /// <param name="schedulerCancellationToken">
        /// The scheduler Cancellation Token.
        /// </param>
        public LinkedCancellationTokensesManager(CancellationToken schedulerCancellationToken)
        {
            ArgumentAssert.NotNull(nameof(schedulerCancellationToken), schedulerCancellationToken);
            ArgumentAssert.NotCanceled(nameof(schedulerCancellationToken), schedulerCancellationToken);

            this.schedulerCancellationToken = schedulerCancellationToken;
        }

        /// <summary>
        /// The create cancellation token source.
        /// </summary>
        /// <returns>
        /// The <see cref="CancellationTokenSource"/>.
        /// </returns>
        protected override CancellationTokenSource CreateCancellationTokenSource()
        {
            return CancellationTokenSource.CreateLinkedTokenSource(this.schedulerCancellationToken);
        }
    }
}
