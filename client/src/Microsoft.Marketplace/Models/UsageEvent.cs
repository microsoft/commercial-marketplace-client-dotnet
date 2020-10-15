// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.Metering.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    public partial class UsageEvent
    {
        /// <summary>
        ///     Check to make sure either resourceUri or resourceId is used but not both before calling the API.
        ///     ResourceId is available only for SaaS offers. Managed application offers can use ResourceId or ResourceUri.
        /// </summary>
        /// <param name="context">Streaming context.</param>
        [OnSerializing]
        internal void CheckPayload(StreamingContext context)
        {
            if (this.ResourceId.HasValue && !string.IsNullOrEmpty(this.ResourceUri))
            {
                throw new ApplicationException("Cannot set resourceId and resourceUri at the same time. Please use ResourceId only if the offer is a SaaS offer. If the offer is a managed application offer, you use either ResourceId or ResourceUri.");
            }
        }
    }
}
