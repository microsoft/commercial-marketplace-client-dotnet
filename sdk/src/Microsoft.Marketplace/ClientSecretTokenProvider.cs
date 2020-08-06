// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Rest;

namespace Microsoft.Marketplace
{
    internal class ClientSecretTokenProvider : MarketplaceTokenProvider
    {
        private readonly string clientSecret;

        internal ClientSecretTokenProvider(Guid tenantId, Guid appId, string clientSecret, string authenticationEndpoint, string marketplaceScope) : base(tenantId, appId, authenticationEndpoint, marketplaceScope)
        {
            this.clientSecret = clientSecret;
        }

        protected override ConfidentialClientApplicationBuilder AddSecret(ConfidentialClientApplicationBuilder builder)
        {
            return builder.WithClientSecret(this.clientSecret);
        }
    }
}