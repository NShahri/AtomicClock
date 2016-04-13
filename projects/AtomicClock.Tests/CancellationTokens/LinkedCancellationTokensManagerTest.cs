// <copyright file="LinkedCancellationTokensManagerTest.cs" company="Nima Shahri">
// Copyright (c) Nima Shahri. All rights reserved.
// </copyright>

namespace AtomicClock.Tests.CancellationTokens
{
    using System;
    using System.Threading;

    using AtomicClock.CancellationTokens;

    using Xunit;

    public class LinkedCancellationTokensManagerTest
    {
        [Fact]
        public void CanceledTokenTest()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.Cancel();
            Assert.Throws<OperationCanceledException>(() => new LinkedCancellationTokensManager(tokenSource.Token));
        }

        public void UnregisterUnknownToken()
        {
            var tokenManager = new LinkedCancellationTokensManager(CancellationToken.None);
            tokenManager.RegisterCancellationToken();

            // NO Exception
            tokenManager.UnregisterCancellationToken(CancellationToken.None);
        }

        [Fact]
        public void UnregisteKnownToken()
        {
            var tokenManager = new LinkedCancellationTokensManager(CancellationToken.None);
            var token = tokenManager.RegisterCancellationToken();

            // NO Exception
            tokenManager.UnregisterCancellationToken(token);
        }

        [Fact]
        public void CancelNoTokenTest()
        {
            var tokenManager = new LinkedCancellationTokensManager(CancellationToken.None);

            // No Exception
            tokenManager.Cancel();
        }

        [Fact]
        public void CancelOneRegisteredTokenTest()
        {
            var tokenManager = new LinkedCancellationTokensManager(CancellationToken.None);
            var token = tokenManager.RegisterCancellationToken();

            tokenManager.Cancel();
            Assert.True(token.IsCancellationRequested);
        }

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
