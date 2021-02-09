// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Microsoft.Marketplace.Metering.Models;

namespace Microsoft.Marketplace.Metering
{
    public interface IMeteringOperations
    {
        Response<IReadOnlyList<UsageEventOkResponse>> PostBatchUsageEvent(IEnumerable<UsageEvent> body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Task<Response<IReadOnlyList<UsageEventOkResponse>>> PostBatchUsageEventAsync(IEnumerable<UsageEvent> body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Response<UsageEventOkResponse> PostUsageEvent(UsageEvent body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);

        Task<Response<UsageEventOkResponse>> PostUsageEventAsync(UsageEvent body, Guid? requestId = null, Guid? correlationId = null, CancellationToken cancellationToken = default);
    }
}