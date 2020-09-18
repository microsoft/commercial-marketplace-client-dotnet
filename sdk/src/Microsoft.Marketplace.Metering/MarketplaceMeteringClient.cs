// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Rest;
using Microsoft.Marketplace.Core;

namespace Microsoft.Marketplace.Metering
{
    public partial class MarketplaceMeteringClient
    {

        /// <summary>
        ///     Preferred constructor if the app is secured with a secret
        /// </summary>
        /// <param name="tenantId">
        ///     Tenant ID of the registered Azure Active Directory app
        /// </param>
        /// <param name="appId">
        ///     App ID (client ID) of the registered Azure Active Directory app
        /// </param>
        /// <param name="clientSecret">
        ///     Client secret/app secret of the registered Azure Active Directory app
        /// </param>
        /// <param name="handlers">
        ///     Custom DelegatingHandlers in case a custom action is required for the request and responses. Usually not a common
        ///     scenario.
        /// </param>
        public MarketplaceMeteringClient(Guid tenantId, Guid appId, string clientSecret, params DelegatingHandler[] handlers) :
            this(
                new TokenCredentials(new ClientSecretTokenProvider(tenantId, appId, clientSecret)), handlers)
        {
        }

        /// <summary>
        ///     Preferred constructor if the app is secured with a secret
        /// </summary>
        /// <param name="tenantId">
        ///     Tenant ID of the registered Azure Active Directory app
        /// </param>
        /// <param name="appId">
        ///     App ID (client ID) of the registered Azure Active Directory app
        /// </param>
        /// <param name="certificate">
        ///     Client certificate of the registered Azure Active Directory app
        /// </param>
        /// <param name="handlers">
        ///     Custom DelegatingHandlers in case a custom action is required for the request and responses. Usually not a common
        ///     scenario.
        /// </param>
        public MarketplaceMeteringClient(Guid tenantId, Guid appId, X509Certificate2 certificate,
            params DelegatingHandler[] handlers) : this(
            new TokenCredentials(new CertificateTokenProvider(tenantId, appId, certificate)), handlers)
        {
        }
    }
}
