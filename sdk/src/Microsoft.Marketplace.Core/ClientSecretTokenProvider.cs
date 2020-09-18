// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using System;
using Microsoft.Identity.Client;

namespace Microsoft.Marketplace.Core
{
    public class ClientSecretTokenProvider : MarketplaceTokenProvider
    {
        private readonly string clientSecret;

        public ClientSecretTokenProvider(Guid tenantId, Guid appId, string clientSecret) : base(tenantId, appId)
        {
            this.clientSecret = clientSecret;
        }

        public override ConfidentialClientApplicationBuilder AddSecret(ConfidentialClientApplicationBuilder builder)
        {
            return builder.WithClientSecret(this.clientSecret);
        }
    }
}