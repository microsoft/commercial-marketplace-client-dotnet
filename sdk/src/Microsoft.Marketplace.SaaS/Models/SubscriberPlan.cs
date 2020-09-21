// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.SaaS.Models
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Check only quantity or plan ID is sent.
    /// </summary>
    public partial class SubscriberPlan
    {
        /// <summary>
        ///     Check to make sure either planId or quantity before calling the API.
        /// </summary>
        /// <param name="context">Streaming context.</param>
        [OnSerializing]
        internal void CheckPayload(StreamingContext context)
        {
            if (this.Quantity.HasValue && !string.IsNullOrEmpty(this.PlanId))
            {
                throw new ApplicationException("Cannot set planId and quantity at the same time. Please use quantity only if the pricing plan is per user and use planId only if the pricing model is flat rate.");
            }
        }
    }
}