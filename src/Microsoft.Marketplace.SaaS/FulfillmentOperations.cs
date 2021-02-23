// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.SaaS
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Azure;
    using Azure.Core;
    using Microsoft.Marketplace.SaaS.Models;

    /// <summary> The Subscription service client. </summary>
    [CodeGenClient("FulfillmentClient")]
    [CodeGenSuppress("UpdateSubscriptionAsync", typeof(Guid), typeof(SubscriberPlan), typeof(Guid), typeof(Guid), typeof(CancellationToken))]
    [CodeGenSuppress("UpdateSubscription", typeof(Guid), typeof(SubscriberPlan), typeof(Guid), typeof(Guid), typeof(CancellationToken))]
    [CodeGenSuppress("DeleteSubscriptionAsync", typeof(Guid), typeof(Guid), typeof(Guid), typeof(CancellationToken))]
    [CodeGenSuppress("DeleteSubscription", typeof(Guid), typeof(Guid), typeof(Guid), typeof(CancellationToken))]
    public partial class FulfillmentOperations : IFulfillmentOperations
    {
        /// <inheritdoc/>
        public virtual async Task<string> UpdateSubscriptionAsync(Guid subscriptionId, SubscriberPlan body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default)
        {
            using var scope = this._clientDiagnostics.CreateScope("FulfillmentOperations.UpdateSubscription");
            scope.Start();
            try
            {
                var result = (await this.RestClient.UpdateSubscriptionAsync(subscriptionId, body, requestId, correlationId, cancellationToken).ConfigureAwait(false)).Headers.OperationLocationUri;
                return ExtractOperationIdFromOperationLocation(result);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <inheritdoc/>
        public virtual string UpdateSubscription(Guid subscriptionId, SubscriberPlan body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default)
        {
            using var scope = this._clientDiagnostics.CreateScope("FulfillmentOperations.UpdateSubscription");
            scope.Start();
            try
            {
                var result = this.RestClient.UpdateSubscription(subscriptionId, body, requestId, correlationId, cancellationToken).Headers.OperationLocationUri;
                return ExtractOperationIdFromOperationLocation(result);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <inheritdoc/>
        public virtual async Task<string> DeleteSubscriptionAsync(Guid subscriptionId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default)
        {
            using var scope = this._clientDiagnostics.CreateScope("FulfillmentOperations.DeleteSubscription");
            scope.Start();
            try
            {
                var result = (await this.RestClient.DeleteSubscriptionAsync(subscriptionId, requestId, correlationId, cancellationToken).ConfigureAwait(false)).Headers.OperationLocationUri;
                return ExtractOperationIdFromOperationLocation(result);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <inheritdoc/>
        public virtual string DeleteSubscription(Guid subscriptionId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default)
        {
            using var scope = this._clientDiagnostics.CreateScope("FulfillmentOperations.DeleteSubscription");
            scope.Start();
            try
            {
                var result = this.RestClient.DeleteSubscription(subscriptionId, requestId, correlationId, cancellationToken).Headers.OperationLocationUri;
                return ExtractOperationIdFromOperationLocation(result);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        private static string ExtractOperationIdFromOperationLocation(string operationLocation)
        {
            if (Uri.TryCreate(operationLocation, UriKind.Absolute, out var operationLocationUri))
            {
                // Operation location looks like this, https://marketplaceapi.microsoft.com/api/saas/subscriptions/de8efdae-6fa2-47ba-7903-3ccda226f82f/operations/abe0bb5e-5e91-4713-b84a-d17a2e7ed3a8?api-version=2018-08-31
                // so it is the last segment, that is 6 which is the operationId
                return operationLocationUri.Segments[6];
            }

            throw new ArgumentException("Not a valid Uri", nameof(operationLocation));
        }
    }
}
