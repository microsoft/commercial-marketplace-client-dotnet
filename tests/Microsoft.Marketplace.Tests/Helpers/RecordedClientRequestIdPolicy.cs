// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.TestFramework;

namespace Microsoft.Marketplace.Tests
{
    /// <summary>
    /// Pipeline policy to verify x-ms-client-request-id and x-ms-client-return-request-id
    /// headers that are echoed back from a request match.
    /// </summary>
    public class RecordedClientRequestIdPolicy : HttpPipelineSynchronousPolicy
    {
        private readonly TestRecording testRecording;
        private readonly string parallelRangePrefix = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordedClientRequestIdPolicy"/> class.
        /// </summary>
        /// <param name="testRecording">Recording.</param>
        /// <param name="parallelRange">Allow paralellel ranges.</param>
        public RecordedClientRequestIdPolicy(TestRecording testRecording, bool parallelRange = false)
        {
            this.testRecording = testRecording ?? throw new System.ArgumentNullException(nameof(testRecording));
            if (parallelRange)
            {
                this.parallelRangePrefix = this.testRecording.GenerateId() + "_";
            }
        }

        /// <summary>
        /// Verify x-ms-client-request-id and x-ms-client-return-request-id headers matches as
        /// x-ms-client-return-request-id is an echo of x-mis-client-request-id.
        /// </summary>
        /// <param name="message">The message that was sent.</param>
        public override void OnSendingRequest(HttpMessage message)
        {
            if (this.parallelRangePrefix != null &&
                message.Request.Headers.TryGetValue("x-ms-range", out string range))
            {
                // If we're transferring a sequence of ranges in parallel, use
                // the same prefix and use the range to differentiate each message
                message.Request.ClientRequestId = this.parallelRangePrefix + range;
            }
            else if (this.parallelRangePrefix != null &&
                message.Request.Uri.Query.Contains("blockid="))
            {
                var queryParameters = message.Request.Uri.Query.Split('&');
                var blockIdParameter = queryParameters.Where(s => s.Contains("blockid=")).First();
                var blockIdValue = blockIdParameter.Split('=')[1];

                message.Request.ClientRequestId = this.parallelRangePrefix + blockIdValue;
            }
            else
            {
                message.Request.ClientRequestId = this.testRecording.Random.NewGuid().ToString();
            }
        }
    }
}
