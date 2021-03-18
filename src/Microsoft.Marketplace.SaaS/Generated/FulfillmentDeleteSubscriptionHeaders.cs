// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using Azure;
using Azure.Core;

namespace Microsoft.Marketplace.SaaS
{
    internal partial class FulfillmentDeleteSubscriptionHeaders
    {
        private readonly Response _response;
        public FulfillmentDeleteSubscriptionHeaders(Response response)
        {
            _response = response;
        }
        public string OperationLocationUri => _response.Headers.TryGetValue("Operation-Location", out string value) ? value : null;
    }
}
