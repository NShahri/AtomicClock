// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CancellationTokensManager.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>
// <summary>
//   Defines the SimpleCancellationTokenManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtomicClock.CancellationTokens
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// The collection cancellation token manager.
    /// </summary>
    internal class CancellationTokensManager : ICancellationTokensManager
    {
        /// <summary>
        /// The cancellation token sources.
        /// </summary>
        private readonly List<CancellationTokenSource> cancellationTokenSources = new List<CancellationTokenSource>();

        /// <summary>
        /// The register cancellation token.
        /// </summary>
        /// <returns>
        /// The <see cref="CancellationToken"/>.
        /// </returns>
        public CancellationToken RegisterCancellationToken()
        {
            var cancellationTokenSource = this.CreateCancellationTokenSource();
            lock (this.cancellationTokenSources)
            {
                this.cancellationTokenSources.Add(cancellationTokenSource);
            }

            cancellationTokenSource.Token.Register(
                () =>
                    {
                        lock (this.cancellationTokenSources)
                        {
                            this.UnregisterCancellationTokenSource(cancellationTokenSource);
                        }
                    });

            return cancellationTokenSource.Token;
        }

        /// <summary>
        /// The cancel.
        /// </summary>
        public void Cancel()
        {
            List<CancellationTokenSource> tokens;
            lock (this.cancellationTokenSources)
            {
                tokens = this.cancellationTokenSources.ToList();
            }

            tokens.ForEach(c => c.Cancel());
        }

        /// <summary>
        /// The unregister cancellation token.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        public void UnregisterCancellationToken(CancellationToken token)
        {
            lock (this.cancellationTokenSources)
            {
                var tokenSource = this.cancellationTokenSources.SingleOrDefault(c => c.Token == token);
                this.UnregisterCancellationTokenSource(tokenSource);
            }
        }

        /// <summary>
        /// The unregister cancellation token source.
        /// </summary>
        /// <param name="tokenSource">
        /// The token source.
        /// </param>
        protected void UnregisterCancellationTokenSource(CancellationTokenSource tokenSource)
        {
            // There is a chance user cancels the token which the task is going to unregister it.
            // because task is completed
            // DO NOTHING is a valid policy in this case
            if (tokenSource == null)
            {
                return;
            }

            if (this.cancellationTokenSources.Remove(tokenSource))
            {
                tokenSource.Dispose();
            }
        }

        /// <summary>
        /// The create cancellation token source.
        /// </summary>
        /// <returns>
        /// The <see cref="CancellationTokenSource"/>.
        /// </returns>
        protected virtual CancellationTokenSource CreateCancellationTokenSource()
        {
            return new CancellationTokenSource();
        }
    }
}
