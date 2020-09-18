// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.


using Microsoft.Identity.Client;
using Microsoft.Rest;
using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Marketplace.Core
{
    public abstract class MarketplaceTokenProvider : ITokenProvider
    {
        private const string authenticationEndpoint = "https://login.microsoftonline.com/";

        // Please see https://docs.microsoft.com/en-us/azure/marketplace/partner-center-portal/pc-saas-registration#get-a-token-based-on-the-azure-ad-app
        private const string marketplaceResourceId = "20e940b3-4c77-4b0b-9a53-9e16a1b010a7";

        protected readonly Guid tenantId;
        protected readonly Guid appId;

        public MarketplaceTokenProvider(Guid tenantId, Guid appId)
        {
            this.tenantId = tenantId;
            this.appId = appId;
        }
        public async Task<AuthenticationHeaderValue> GetAuthenticationHeaderAsync(CancellationToken cancellationToken)
        {
            var builder = ConfidentialClientApplicationBuilder
                .Create(this.appId.ToString())
                .WithAuthority(authenticationEndpoint + this.tenantId);

            var adApp = this.AddSecret(builder).Build();

            var scopes = new [] { $"{marketplaceResourceId}/.default" };

            var result = await adApp.AcquireTokenForClient(scopes).ExecuteAsync();
            return new AuthenticationHeaderValue("Bearer", result.AccessToken);
        }

        public abstract ConfidentialClientApplicationBuilder AddSecret(ConfidentialClientApplicationBuilder builder);
    }
}
