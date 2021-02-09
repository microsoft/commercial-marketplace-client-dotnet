// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.SaaS
{
    using System;
    using Azure.Core;
    using Azure.Core.Pipeline;

    /// <summary> SaaS service management client. </summary>
    public class MarketplaceSaaSClient : IMarketplaceSaaSClient
    {
        private readonly ClientDiagnostics clientDiagnostics;
        private readonly HttpPipeline pipeline;
        private readonly Uri endpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketplaceSaaSClient"/> class.
        /// </summary>
        /// <param name="tokenCredential">Token credential.</param>
        /// <param name="options">Client options.</param>
        public MarketplaceSaaSClient(TokenCredential tokenCredential, MarketplaceSaaSClientOptions options = null)
            : this(null, tokenCredential, options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketplaceSaaSClient"/> class.
        /// </summary>
        protected MarketplaceSaaSClient()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MarketplaceSaaSClient"/> class. Initializes a new instance of SaaSManagementClient. </summary>
        /// <param name="endpoint"> server parameter. </param>
        /// <param name="tokenCredential"> The OAuth token for making client requests. </param>
        /// <param name="options"> The options for configuring the client. </param>
#pragma warning disable SA1202 // Elements should be ordered by access
        public MarketplaceSaaSClient(Uri endpoint, TokenCredential tokenCredential, MarketplaceSaaSClientOptions options = null)
#pragma warning restore SA1202 // Elements should be ordered by access
        {
            endpoint ??= new Uri("https://marketplaceapi.microsoft.com/api");

            options ??= new MarketplaceSaaSClientOptions();
            this.clientDiagnostics = new ClientDiagnostics(options);
            this.pipeline = HttpPipelineBuilder.Build(options, new BearerTokenAuthenticationPolicy(tokenCredential, $"20e940b3-4c77-4b0b-9a53-9e16a1b010a7/.default"));
            this.endpoint = endpoint;
        }

        /// <summary> Gets an instance of FulfillmentOperations. </summary>
        public virtual FulfillmentOperations Fulfillment => new FulfillmentOperations(this.clientDiagnostics, this.pipeline, this.endpoint);

        /// <summary> Gets an instance of Operations. </summary>
        public virtual SubscriptionOperations Operations => new SubscriptionOperations(this.clientDiagnostics, this.pipeline, this.endpoint);
    }
}
