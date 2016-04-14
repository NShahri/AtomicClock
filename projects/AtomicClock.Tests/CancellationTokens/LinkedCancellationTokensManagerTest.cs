// <copyright file="LinkedCancellationTokensManagerTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>

namespace AtomicClock.Tests.CancellationTokens
{
    using System;
    using System.Threading;

    using AtomicClock.CancellationTokens;

    using Xunit;

    /// <summary>
    /// TEsts for LinkedCancellationTokensManager
    /// </summary>
    public class LinkedCancellationTokensManagerTest
    {
        /// <summary>
        /// Canceleds the token test.
        /// </summary>
        [Fact]
        public void CanceledTokenTest()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.Cancel();
            Assert.Throws<OperationCanceledException>(() => new LinkedCancellationTokensManager(tokenSource.Token));
        }

        /// <summary>
        /// Unregisters the unknown token.
        /// </summary>
        public void UnregisterUnknownToken()
        {
            var tokenManager = new LinkedCancellationTokensManager(CancellationToken.None);
            tokenManager.RegisterCancellationToken();

            // NO Exception
            tokenManager.UnregisterCancellationToken(CancellationToken.None);
        }

        /// <summary>
        /// Unregistes the known token.
        /// </summary>
        [Fact]
        public void UnregisteKnownToken()
        {
            var tokenManager = new LinkedCancellationTokensManager(CancellationToken.None);
            var token = tokenManager.RegisterCancellationToken();

            // NO Exception
            tokenManager.UnregisterCancellationToken(token);
        }

        /// <summary>
        /// Cancels the no token test.
        /// </summary>
        [Fact]
        public void CancelNoTokenTest()
        {
            var tokenManager = new LinkedCancellationTokensManager(CancellationToken.None);

            // No Exception
            tokenManager.Cancel();
        }

        /// <summary>
        /// Cancels the one registered token test.
        /// </summary>
        [Fact]
        public void CancelOneRegisteredTokenTest()
        {
            var tokenManager = new LinkedCancellationTokensManager(CancellationToken.None);
            var token = tokenManager.RegisterCancellationToken();

            tokenManager.Cancel();
            Assert.True(token.IsCancellationRequested);
        }

        /// <summary>
        /// Cancels the two registered tokens test.
        /// </summary>
        [Fact]
        public void CancelTwoRegisteredTokensTest()
        {
            var tokenManager = new LinkedCancellationTokensManager(CancellationToken.None);
            var token1 = tokenManager.RegisterCancellationToken();
            var token2 = tokenManager.RegisterCancellationToken();

            tokenManager.Cancel();
            Assert.True(token1.IsCancellationRequested);
            Assert.True(token2.IsCancellationRequested);
        }

        /// <summary>
        /// Cancels the linked cancelation token test.
        /// </summary>
        [Fact]
        public void CancelLinkedCancelationTokenTest()
        {
            var tokenSource = new CancellationTokenSource();

            var tokenManager = new LinkedCancellationTokensManager(tokenSource.Token);
            var token = tokenManager.RegisterCancellationToken();

            tokenSource.Cancel();
            Assert.True(token.IsCancellationRequested);
        }
    }
}
