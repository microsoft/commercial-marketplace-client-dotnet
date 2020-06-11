// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Marketplace
{
    using Microsoft.Rest;
    using Microsoft.Rest.Azure;
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// MeteringOperations operations.
    /// </summary>
    public partial interface IMeteringOperations
    {
        /// <summary>
        /// Posts a single usage event to the marketplace metering service API.
        /// </summary>
        /// <remarks>
        /// Posts a single usage event to the marketplace metering service API.
        /// </remarks>
        /// <param name='body'>
        /// </param>
        /// <param name='requestId'>
        /// A unique string value for tracking the request from the client,
        /// preferably a GUID. If this value isn't provided, one will be
        /// generated and provided in the response headers.
        /// </param>
        /// <param name='correlationId'>
        /// A unique string value for operation on the client. This parameter
        /// correlates all events from client operation with events on the
        /// server side. If this value isn't provided, one will be generated
        /// and provided in the response headers.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.Azure.CloudException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        Task<AzureOperationResponse<UsageEventOkResponse>> PostUsageEventWithHttpMessagesAsync(IList<UsageEvent> body, System.Guid? requestId = default(System.Guid?), System.Guid? correlationId = default(System.Guid?), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Posts a set of usage events to the marketplace metering service
        /// API.
        /// </summary>
        /// <remarks>
        /// The batch usage event API allows you to emit usage events for more
        /// than one purchased entity at once. The batch usage event request
        /// references the metering services dimension defined by the publisher
        /// when publishing the offer.
        /// </remarks>
        /// <param name='body'>
        /// </param>
        /// <param name='requestId'>
        /// A unique string value for tracking the request from the client,
        /// preferably a GUID. If this value isn't provided, one will be
        /// generated and provided in the response headers.
        /// </param>
        /// <param name='correlationId'>
        /// A unique string value for operation on the client. This parameter
        /// correlates all events from client operation with events on the
        /// server side. If this value isn't provided, one will be generated
        /// and provided in the response headers.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.Azure.CloudException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        Task<AzureOperationResponse<UsageEventOkResponse>> PostBatchUsageEventWithHttpMessagesAsync(IList<UsageEvent> body, System.Guid? requestId = default(System.Guid?), System.Guid? correlationId = default(System.Guid?), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
