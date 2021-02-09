// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Marketplace.Metering
{
    /// <summary> Metering service management client. </summary>
    public interface IMeteringManagementClient
    {
        /// <summary> Gets an instance of MeteringOperations. </summary>
        MeteringOperations Metering { get; }
    }
}