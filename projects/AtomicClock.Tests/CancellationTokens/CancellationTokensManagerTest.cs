// <copyright file="CancellationTokensManagerTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>

namespace AtomicClock.Tests.CancellationTokens
{
    using System;
    using System.Threading;

    using AtomicClock.CancellationTokens;

    using Xunit;

    /// <summary>
    /// TEsts for CancellationTokensManager
    /// </summary>
    public class CancellationTokensManagerTest
    {
        /// <summary>
        /// Unregisters the unknown token.
        /// </summary>
        [Fact]
        public void UnregisterUnknownToken()
        {
            var tokenManager = new CancellationTokensManager();
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
            var tokenManager = new CancellationTokensManager();
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
            var tokenManager = new CancellationTokensManager();

            // No Exception
            tokenManager.Cancel();
        }

        /// <summary>
        /// Cancels the one registered token test.
        /// </summary>
        [Fact]
        public void CancelOneRegisteredTokenTest()
        {
            var tokenManager = new CancellationTokensManager();
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
            var tokenManager = new CancellationTokensManager();
            var token1 = tokenManager.RegisterCancellationToken();
            var token2 = tokenManager.RegisterCancellationToken();

            tokenManager.Cancel();
            Assert.True(token1.IsCancellationRequested);
            Assert.True(token2.IsCancellationRequested);
        }
    }
}
