// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Microsoft.Marketplace.Metering.Models
{
    /// <summary> The GetUsageEvent. </summary>
    public partial class GetUsageEvent
    {
        /// <summary> Initializes a new instance of GetUsageEvent. </summary>
        internal GetUsageEvent()
        {
        }

        /// <summary> Initializes a new instance of GetUsageEvent. </summary>
        /// <param name="usageDate"> Time in UTC when the usage event occurred. </param>
        /// <param name="usageResourceId"> Identifier of the resource against which usage is emitted. </param>
        /// <param name="dimension"> Dimension. </param>
        /// <param name="planId"> PlanId. </param>
        /// <param name="planName"> PlanName. </param>
        /// <param name="offerId"> OfferId. </param>
        /// <param name="offerName"> OfferName. </param>
        /// <param name="offerType"> OfferType. </param>
        /// <param name="azureSubscriptionId"> Azure Subscription Id. </param>
        /// <param name="reconStatus"> Recon Status. </param>
        /// <param name="submittedQuantity"> Submitted Quantity. </param>
        /// <param name="processedQuantity"> Processed Quantity. </param>
        /// <param name="submittedCount"> Submitted count. </param>
        internal GetUsageEvent(DateTimeOffset? usageDate, Guid? usageResourceId, string dimension, string planId, string planName, string offerId, string offerName, string offerType, Guid? azureSubscriptionId, ReconStatusEnum? reconStatus, double? submittedQuantity, double? processedQuantity, double? submittedCount)
        {
            UsageDate = usageDate;
            UsageResourceId = usageResourceId;
            Dimension = dimension;
            PlanId = planId;
            PlanName = planName;
            OfferId = offerId;
            OfferName = offerName;
            OfferType = offerType;
            AzureSubscriptionId = azureSubscriptionId;
            ReconStatus = reconStatus;
            SubmittedQuantity = submittedQuantity;
            ProcessedQuantity = processedQuantity;
            SubmittedCount = submittedCount;
        }

        /// <summary> Time in UTC when the usage event occurred. </summary>
        public DateTimeOffset? UsageDate { get; }
        /// <summary> Identifier of the resource against which usage is emitted. </summary>
        public Guid? UsageResourceId { get; }
        /// <summary> Dimension. </summary>
        public string Dimension { get; }
        /// <summary> PlanId. </summary>
        public string PlanId { get; }
        /// <summary> PlanName. </summary>
        public string PlanName { get; }
        /// <summary> OfferId. </summary>
        public string OfferId { get; }
        /// <summary> OfferName. </summary>
        public string OfferName { get; }
        /// <summary> OfferType. </summary>
        public string OfferType { get; }
        /// <summary> Azure Subscription Id. </summary>
        public Guid? AzureSubscriptionId { get; }
        /// <summary> Recon Status. </summary>
        public ReconStatusEnum? ReconStatus { get; }
        /// <summary> Submitted Quantity. </summary>
        public double? SubmittedQuantity { get; }
        /// <summary> Processed Quantity. </summary>
        public double? ProcessedQuantity { get; }
        /// <summary> Submitted count. </summary>
        public double? SubmittedCount { get; }
    }
}
