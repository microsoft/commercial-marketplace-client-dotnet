// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using Microsoft.Identity.Client;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Marketplace.Core
{
    public class CertificateTokenProvider : MarketplaceTokenProvider
    {
        private readonly X509Certificate2 certificate;

        public CertificateTokenProvider(Guid tenantId, Guid appId, X509Certificate2 certificate) : base(tenantId, appId)
        {
            this.certificate = certificate;
        }

        public override ConfidentialClientApplicationBuilder AddSecret(ConfidentialClientApplicationBuilder builder)
        {
            return builder.WithCertificate(this.certificate);
        }
    }
}