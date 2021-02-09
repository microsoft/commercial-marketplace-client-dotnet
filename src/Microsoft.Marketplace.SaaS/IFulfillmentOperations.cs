// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Microsoft.Marketplace.SaaS.Models;

namespace Microsoft.Marketplace.SaaS
{
    /// <summary> The Subscription service client. </summary>
    public interface IFulfillmentOperations
    {
        Response ActivateSubscription(Guid subscriptionId, SubscriberPlan body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Task<Response> ActivateSubscriptionAsync(Guid subscriptionId, SubscriberPlan body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        /// <summary> Unsubscribe and delete the specified subscription. </summary>
        /// <param name="subscriptionId"> The Uuid to use. </param>
        /// <param name="requestId"> A unique string value for tracking the request from the client, preferably a GUID. If this value isn&apos;t provided, one will be generated and provided in the response headers. </param>
        /// <param name="correlationId"> A unique string value for operation on the client. This parameter correlates all events from client operation with events on the server side. If this value isn&apos;t provided, one will be generated and provided in the response headers. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns>A <see cref="string"/> representing the result of the  operation.</returns>
        string DeleteSubscription(Guid subscriptionId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        /// <summary> Unsubscribe and delete the specified subscription. </summary>
        /// <param name="subscriptionId"> The Uuid to use. </param>
        /// <param name="requestId"> A unique string value for tracking the request from the client, preferably a GUID. If this value isn&apos;t provided, one will be generated and provided in the response headers. </param>
        /// <param name="correlationId"> A unique string value for operation on the client. This parameter correlates all events from client operation with events on the server side. If this value isn&apos;t provided, one will be generated and provided in the response headers. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> DeleteSubscriptionAsync(Guid subscriptionId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Response<Subscription> GetSubscription(Guid subscriptionId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Task<Response<Subscription>> GetSubscriptionAsync(Guid subscriptionId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Response<SubscriptionPlans> ListAvailablePlans(Guid subscriptionId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Task<Response<SubscriptionPlans>> ListAvailablePlansAsync(Guid subscriptionId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Pageable<Subscription> ListSubscriptions(string continuationToken = null, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        AsyncPageable<Subscription> ListSubscriptionsAsync(string continuationToken = null, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Response<ResolvedSubscription> Resolve(string xMsMarketplaceToken, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Task<Response<ResolvedSubscription>> ResolveAsync(string xMsMarketplaceToken, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        /// <summary> Use this call to update the plan, the user count (quantity), or both. </summary>
        /// <param name="subscriptionId"> The Uuid to use. </param>
        /// <param name="body"> The SubscriberPlan to use. </param>
        /// <param name="requestId"> A unique string value for tracking the request from the client, preferably a GUID. If this value isn&apos;t provided, one will be generated and provided in the response headers. </param>
        /// <param name="correlationId"> A unique string value for operation on the client. This parameter correlates all events from client operation with events on the server side. If this value isn&apos;t provided, one will be generated and provided in the response headers. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns>A <see cref="string"/> representing the result of the  operation.</returns>
        string UpdateSubscription(Guid subscriptionId, SubscriberPlan body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        /// <summary> Use this call to update the plan, the user count (quantity), or both. </summary>
        /// <param name="subscriptionId"> The Uuid to use. </param>
        /// <param name="body"> The SubscriberPlan to use. </param>
        /// <param name="requestId"> A unique string value for tracking the request from the client, preferably a GUID. If this value isn&apos;t provided, one will be generated and provided in the response headers. </param>
        /// <param name="correlationId"> A unique string value for operation on the client. This parameter correlates all events from client operation with events on the server side. If this value isn&apos;t provided, one will be generated and provided in the response headers. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns>A <see cref="Task{string}"/> representing the result of the asynchronous operation.</returns>
        Task<string> UpdateSubscriptionAsync(Guid subscriptionId, SubscriberPlan body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);
    }
}