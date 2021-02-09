// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Microsoft.Marketplace.SaaS.Models;

namespace Microsoft.Marketplace.SaaS
{
    public interface ISubscriptionOperations
    {
        Response<Operation> GetOperationStatus(Guid subscriptionId, Guid operationId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Task<Response<Operation>> GetOperationStatusAsync(Guid subscriptionId, Guid operationId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Response<OperationList> ListOperations(Guid subscriptionId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Task<Response<OperationList>> ListOperationsAsync(Guid subscriptionId, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Response UpdateOperationStatus(Guid subscriptionId, Guid operationId, UpdateOperation body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Task<Response> UpdateOperationStatusAsync(Guid subscriptionId, Guid operationId, UpdateOperation body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);
    }
}