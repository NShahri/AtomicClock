// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICancellationTokensManager.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
// </copyright>
// <summary>
//   Defines the ICancellationTokensManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.CancellationTokens
{
    using System.Threading;

    /// <summary>
    /// The CancellationTokenManager interface.
    /// </summary>
    public interface ICancellationTokensManager
    {
        /// <summary>
        /// The register cancellation token.
        /// </summary>
        /// <returns>
        /// The <see cref="CancellationToken"/>.
        /// </returns>
        CancellationToken RegisterCancellationToken();

        /// <summary>
        /// The cancel.
        /// </summary>
        void Cancel();

        /// <summary>
        /// The unregister cancellation token.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        void UnregisterCancellationToken(CancellationToken token);
    }
}
