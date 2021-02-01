// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable SA1200 // Using directives should be placed correctly
using Azure.Core;
#pragma warning restore SA1200 // Using directives should be placed correctly
using Azure.Core.Pipeline;
using Azure.Core.TestFramework;
using System.Linq;

namespace Microsoft.Marketplace.Tests
{
    /// <summary>
    /// Pipeline policy to verify x-ms-client-request-id and x-ms-client-return-request-id
    /// headers that are echoed back from a request match.
    /// </summary>
    public class RecordedClientRequestIdPolicy : HttpPipelineSynchronousPolicy
    {
        private readonly TestRecording _testRecording;
        private readonly string _parallelRangePrefix = null;

        /// <summary>
        /// Create a new RecordedClientRequestIdPolicy
        /// </summary>
        public RecordedClientRequestIdPolicy(TestRecording testRecording, bool parallelRange = false)
        {
            _testRecording = testRecording;
            if (parallelRange)
            {
                _parallelRangePrefix = _testRecording.GenerateId() + "_";
            }
        }

        /// <summary>
        /// Verify x-ms-client-request-id and x-ms-client-return-request-id headers matches as
        /// x-ms-client-return-request-id is an echo of x-mis-client-request-id.
        /// </summary>
        /// <param name="message">The message that was sent</param>
        public override void OnSendingRequest(HttpMessage message)
        {
            if (_parallelRangePrefix != null &&
                message.Request.Headers.TryGetValue("x-ms-range", out string range))
            {
                // If we're transferring a sequence of ranges in parallel, use
                // the same prefix and use the range to differentiate each message
                message.Request.ClientRequestId = _parallelRangePrefix + range;
            }
            else if (_parallelRangePrefix != null &&
                message.Request.Uri.Query.Contains("blockid="))
            {
                var queryParameters = message.Request.Uri.Query.Split('&');
                var blockIdParameter = queryParameters.Where(s => s.Contains("blockid=")).First();
                var blockIdValue = blockIdParameter.Split('=')[1];

                message.Request.ClientRequestId = _parallelRangePrefix + blockIdValue;
            }
            else
            {
                message.Request.ClientRequestId = _testRecording.Random.NewGuid().ToString();
            }
        }
    }
}
