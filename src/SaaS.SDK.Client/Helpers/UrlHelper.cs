﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for license information.
namespace Microsoft.Marketplace.Client.Helpers
{
    using System;
    using Microsoft.Marketplace.Client.Configurations;
    using Microsoft.Marketplace.Client.Models;

    /// <summary>
    /// The url helper.
    /// </summary>
    public class UrlHelper
    {
        /// <summary>
        /// GetFulfilllments the URL.
        /// </summary>
        /// <param name="clientConfiguration">The client configuration.</param>
        /// <param name="resourceGuid">The resource unique identifier.</param>
        /// <param name="action">The action.</param>
        /// <param name="operationGuid">The operation unique identifier.</param>
        /// <returns> Saas URL.</returns>
        public static string GetSaaSApiUrl(SaaSApiClientConfiguration clientConfiguration, Guid resourceGuid, SaaSResourceActionEnum? action, Guid? operationGuid = null)
        {
            var resourceId = Convert.ToString(resourceGuid);
            string operationId = string.Empty;
            string subscriptionBaseURL = "/saas/subscriptions/";
            if (operationGuid != null && operationGuid != (Guid)default)
            {
                operationId = Convert.ToString(operationGuid);
            }

            switch (action)
            {
                case SaaSResourceActionEnum.RESOLVE:
                    return $"{clientConfiguration.FulFillmentAPIBaseURL}{subscriptionBaseURL}resolve?api-version={clientConfiguration.FulFillmentAPIVersion}";
                case SaaSResourceActionEnum.ACTIVATE:
                    return $"{clientConfiguration.FulFillmentAPIBaseURL}{subscriptionBaseURL}{resourceId}/activate?api-version={clientConfiguration.FulFillmentAPIVersion}";
                case SaaSResourceActionEnum.LISTALLPLAN:
                    return $"{clientConfiguration.FulFillmentAPIBaseURL}{subscriptionBaseURL}{resourceId}/listAvailablePlans?api-version={clientConfiguration.FulFillmentAPIVersion}";
                case SaaSResourceActionEnum.OPERATION_STATUS:
                    return $"{clientConfiguration.FulFillmentAPIBaseURL}{subscriptionBaseURL}{resourceId}/operations/{operationId}?api-version={clientConfiguration.FulFillmentAPIVersion}";
                case SaaSResourceActionEnum.ALL_SUBSCRIPTIONS:
                    return $"{clientConfiguration.FulFillmentAPIBaseURL}{subscriptionBaseURL}?api-version={clientConfiguration.FulFillmentAPIVersion}";
                case SaaSResourceActionEnum.SUBSCRIPTION_USAGEEVENT:
                    return $"{clientConfiguration.FulFillmentAPIBaseURL}/usageEvent?api-version={clientConfiguration.FulFillmentAPIVersion}";
                case SaaSResourceActionEnum.SUBSCRIPTION_BATCHUSAGEEVENT:
                    return $"{clientConfiguration.FulFillmentAPIBaseURL}/batchUsageEvent?api-version={clientConfiguration.FulFillmentAPIVersion}";
                default:
                    return $"{clientConfiguration.FulFillmentAPIBaseURL}{subscriptionBaseURL}{resourceId}?api-version={clientConfiguration.FulFillmentAPIVersion}";
            }
        }
    }
}
