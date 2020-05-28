// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for license information.
namespace Microsoft.Marketplace.SaaS.Client.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Sets Plan.
    /// </summary>
    /// <seealso cref="Microsoft.Marketplace.SaaS.Client.Models.SaaSApiResult" />
    public class PlanResult : SaaSApiResult
    {
        /// <summary>
        /// Gets or sets the plans.
        /// </summary>
        /// <value>
        /// The plans.
        /// </value>
        [JsonPropertyName("plans")]
        [DisplayName("plans")]
        public List<PlanDetailResult> Plans { get; set; }
    }
}
