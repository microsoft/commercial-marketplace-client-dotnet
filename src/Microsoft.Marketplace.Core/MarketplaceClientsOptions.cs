// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.Core
{
    using System;
    using Azure.Core;

    /// <summary>
    /// Client options for marketplace clients.
    /// </summary>
    public abstract class MarketplaceClientsOptions : ClientOptions
    {
        // Marketpace API version.
#pragma warning disable SA1600 // Elements should be documented
        internal const ServiceVersion LatestVersion = ServiceVersion.V2_0;
#pragma warning restore SA1600 // Elements should be documented

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketplaceClientsOptions"/> class.
        /// </summary>
        /// <param name="version">Service version.</param>
        public MarketplaceClientsOptions(ServiceVersion version = LatestVersion)
        {
            this.Version = version;
        }

        /// <summary>
        /// The service version.
        /// </summary>
        public enum ServiceVersion
        {
            /// <summary>
            /// The V2.0 of the service.
            /// </summary>
            V2_0 = 2,
        }

        /// <summary>
        /// Gets the service version.
        /// </summary>
        public ServiceVersion Version { get; }

        /// <summary>
        /// Gets the version string.
        /// </summary>
        /// <param name="version">Version.</param>
        /// <returns>string.</returns>
        internal static string GetVersionString(ServiceVersion version)
        {
            return version switch
            {
                ServiceVersion.V2_0 => "v2.0",
                _ => throw new NotSupportedException($"The service version {version} is not supported."),
            };
        }
    }
}
