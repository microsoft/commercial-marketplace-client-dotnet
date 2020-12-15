// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Azure.Core;
using System;

namespace Microsoft.Marketplace.Core
{
    public abstract class MarketplaceClientsOptions : ClientOptions
    {
        internal const ServiceVersion LatestVersion = ServiceVersion.V2_0;

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
#pragma warning disable CA1707 // Identifiers should not contain underscores
            V2_0 = 2
#pragma warning restore CA1707 // Identifiers should not contain underscores
        }

        /// <summary>
        /// The service version.
        /// </summary>
        public ServiceVersion Version { get; }

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
