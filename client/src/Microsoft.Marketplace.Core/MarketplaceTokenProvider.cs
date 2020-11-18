// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.Core
{
    using System;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Identity.Client;
    using Microsoft.Rest;

    /// <summary>
    /// Parent access token provider for marketplace clients.
    /// </summary>
    public abstract class MarketplaceTokenProvider : ITokenProvider
    {
        private const string AuthenticationEndpoint = "https://login.microsoftonline.com/";

        // Please see https://docs.microsoft.com/en-us/azure/marketplace/partner-center-portal/pc-saas-registration#get-a-token-based-on-the-azure-ad-app
        private const string MarketplaceResourceId = "20e940b3-4c77-4b0b-9a53-9e16a1b010a7";

        private readonly Guid tenantId;
        private readonly Guid appId;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketplaceTokenProvider"/> class.
        /// </summary>
        /// <param name="tenantId">Tenant Id of the app registration.</param>
        /// <param name="appId">App Id of the app registration.</param>
        public MarketplaceTokenProvider(Guid tenantId, Guid appId)
        {
            this.tenantId = tenantId;
            this.appId = appId;
        }

        /// <summary>
        /// Return the authorization header value.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Authorization header.</returns>
        public async Task<AuthenticationHeaderValue> GetAuthenticationHeaderAsync(CancellationToken cancellationToken)
        {
            var builder = ConfidentialClientApplicationBuilder
                .Create(this.appId.ToString())
                .WithAuthority(new Uri(AuthenticationEndpoint + this.tenantId));

            var adApp = this.AddSecret(builder).Build();

            var scopes = new[] { $"{MarketplaceResourceId}/.default" };

            var result = await adApp.AcquireTokenForClient(scopes).ExecuteAsync(cancellationToken).ConfigureAwait(false);
            return new AuthenticationHeaderValue("Bearer", result.AccessToken);
        }

        /// <summary>
        /// Abstract method to be implemented for adding the secret, can be app key or certificate.
        /// </summary>
        /// <param name="builder">MSAL applicaiton builder.</param>
        /// <returns>MSAL Application builder.</returns>
        public abstract ConfidentialClientApplicationBuilder AddSecret(ConfidentialClientApplicationBuilder builder);
    }
}
