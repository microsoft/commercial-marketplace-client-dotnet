// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.Core
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.Identity.Client;

    /// <summary>
    /// Token provider using a client certificate.
    /// </summary>
    public class CertificateTokenProvider : MarketplaceTokenProvider
    {
        private readonly X509Certificate2 certificate;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateTokenProvider"/> class.
        /// </summary>
        /// <param name="tenantId">Id of the AD tenant where the app is registered.</param>
        /// <param name="appId">App Id (client Id) of the AAD app registration.</param>
        /// <param name="certificate">Client certificate for the registred app.</param>
        public CertificateTokenProvider(Guid tenantId, Guid appId, X509Certificate2 certificate)
            : base(tenantId, appId)
        {
            this.certificate = certificate;
        }

        /// <inheritdoc/>
        public override ConfidentialClientApplicationBuilder AddSecret(ConfidentialClientApplicationBuilder builder)
        {
            if (builder == default)
            {
                throw new ArgumentNullException(paramName: nameof(builder));
            }

            return builder.WithCertificate(this.certificate);
        }
    }
}