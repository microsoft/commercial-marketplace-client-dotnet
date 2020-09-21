// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.SaaS.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Add OperationLocation property to the header class.
    /// </summary>
    public partial class FulfillmentOperationsUpdateSubscriptionHeaders
    {
        /// <summary>
        /// Gets operation Id from OperationLocation property.
        /// </summary>
        [JsonIgnore]
        public Guid OperationId
        {
            get
            {
                // Operation-Location header returns a URI like the followign
                // https://marketplaceapi.microsoft.com/api/saas/subscriptions/4c49739f-217f-5d1b-5722-69278d4015c1/operations/749eb64f-4d7a-4f1d-9bcd-7629b97a2d38?api-version=2018-08-31
                if (!Uri.TryCreate(this.OperationLocationUri, UriKind.Absolute, out var uri) || uri.Segments.Length != 7)
                {
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    throw new ApplicationException($"Operation location received {this.OperationLocationUri} does not have 7 segments.");
                }
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations

                if (Guid.TryParse(uri.Segments[6], out var operationId))
                {
                    return operationId;
                }

#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                throw new ApplicationException($"Cannot parse the operation ID {uri.Segments[6]} as a  Guid");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
            }
        }
    }
}