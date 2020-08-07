// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.


using Microsoft.Identity.Client;
using Microsoft.Rest;
using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Marketplace
{
    internal abstract class MarketplaceTokenProvider : ITokenProvider
    {
        protected readonly Guid tenantId;
        protected readonly Guid appId;
        protected readonly string authenticationEndpoint;
        protected readonly string marketplaceScope;

        protected MarketplaceTokenProvider(Guid tenantId, Guid appId, string authenticationEndpoint, string marketplaceScope)
        {
            this.tenantId = tenantId;
            this.appId = appId;
            this.authenticationEndpoint = authenticationEndpoint;
            this.marketplaceScope = marketplaceScope;
        }
        public async Task<AuthenticationHeaderValue> GetAuthenticationHeaderAsync(CancellationToken cancellationToken)
        {
            var builder = ConfidentialClientApplicationBuilder
                .Create(this.appId.ToString())
                .WithAuthority(this.authenticationEndpoint + this.tenantId);

            var adApp = this.AddSecret(builder).Build();

            var scopes = new [] { $"{this.marketplaceScope}/.default" };

            var result = await adApp.AcquireTokenForClient(scopes).ExecuteAsync();
            return new AuthenticationHeaderValue("Bearer", result.AccessToken);
        }

        protected abstract ConfidentialClientApplicationBuilder AddSecret(ConfidentialClientApplicationBuilder builder);
    }
}
