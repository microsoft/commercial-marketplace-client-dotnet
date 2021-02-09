// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Marketplace.Metering
{
    /// <summary> Metering client. </summary>
    public interface IMarketplaceMeteringClient
    {
        /// <summary> Gets an instance of MeteringOperations. </summary>
        MeteringOperations Metering { get; }
    }
}