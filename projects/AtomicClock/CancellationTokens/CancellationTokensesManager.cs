// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CancellationTokensesManager.cs" company="Nima Shahri">
//   Copyright ©2016. All rights reserved.
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
    internal class CancellationTokensesManager : ICancellationTokensManager
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
                            this.cancellationTokenSources.Remove(cancellationTokenSource);
                        }

                        cancellationTokenSource.Dispose();
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
                var tokenSource = this.cancellationTokenSources.Single(c => c.Token == token);
                this.cancellationTokenSources.Remove(tokenSource);
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
