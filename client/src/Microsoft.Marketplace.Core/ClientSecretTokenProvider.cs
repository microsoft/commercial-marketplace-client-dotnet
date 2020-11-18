// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.Core
{
    using System;
    using Microsoft.Identity.Client;

    /// <summary>
    /// Token provider using a client secret.
    /// </summary>
    public class ClientSecretTokenProvider : MarketplaceTokenProvider
    {
        private readonly string clientSecret;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientSecretTokenProvider"/> class.
        /// </summary>
        /// <param name="tenantId">Id of the AD tenant where the app is registered.</param>
        /// <param name="appId">App Id (client Id) of the AAD app registration.</param>
        /// <param name="clientSecret">Client secret for the app registration.</param>
        public ClientSecretTokenProvider(Guid tenantId, Guid appId, string clientSecret)
            : base(tenantId, appId)
        {
            this.clientSecret = clientSecret;
        }

        /// <inheritdoc/>
        public override ConfidentialClientApplicationBuilder AddSecret(ConfidentialClientApplicationBuilder builder)
        {
            if (builder == default)
            {
                throw new ArgumentNullException(paramName: nameof(builder));
            }

            return builder.WithClientSecret(this.clientSecret);
        }
    }
}