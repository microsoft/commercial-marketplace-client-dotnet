// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.Metering
{
    using System;
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.Marketplace.Core;
    using Microsoft.Rest;

/// <summary>
/// Metering client contructors to pass in token providers.
/// </summary>
    public partial class MarketplaceMeteringClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketplaceMeteringClient"/> class.
        /// </summary>
        /// <param name="tenantId">
        ///     Tenant ID of the registered Azure Active Directory app.
        /// </param>
        /// <param name="appId">
        ///     App ID (client ID) of the registered Azure Active Directory app.
        /// </param>
        /// <param name="clientSecret">
        ///     Client secret/app secret of the registered Azure Active Directory app.
        /// </param>
        /// <param name="handlers">
        ///     Custom DelegatingHandlers in case a custom action is required for the request and responses. Usually not a common
        ///     scenario.
        /// </param>
        public MarketplaceMeteringClient(Guid tenantId, Guid appId, string clientSecret, params DelegatingHandler[] handlers)
            : this(new TokenCredentials(new ClientSecretTokenProvider(tenantId, appId, clientSecret)), handlers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketplaceMeteringClient"/> class.
        /// </summary>
        /// <param name="tenantId">
        ///     Tenant ID of the registered Azure Active Directory app.
        /// </param>
        /// <param name="appId">
        ///     App ID (client ID) of the registered Azure Active Directory app.
        /// </param>
        /// <param name="certificate">
        ///     Client certificate of the registered Azure Active Directory app.
        /// </param>
        /// <param name="handlers">
        ///     Custom DelegatingHandlers in case a custom action is required for the request and responses. Usually not a common
        ///     scenario.
        /// </param>
        public MarketplaceMeteringClient(Guid tenantId, Guid appId, X509Certificate2 certificate, params DelegatingHandler[] handlers)
                : this(new TokenCredentials(new CertificateTokenProvider(tenantId, appId, certificate)), handlers)
        {
        }
    }
}
