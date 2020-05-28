// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for license information.
namespace Microsoft.Marketplace.SaaS.Client.Network
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Marketplace.SaaS.SDK.Client.Network;
    using Microsoft.Marketplace.Client.Configurations;
    using Microsoft.Marketplace.Client.Contracts;
    using Microsoft.Marketplace.Client.Helpers;
    using Microsoft.Marketplace.Client.Models;

    /// <summary>
    /// rest client call implementation.
    /// </summary>
    /// <typeparam name="T">type of entity.</typeparam>
    public abstract class AbstractSaaSApiRestClient<T>
        where T : SaaSApiResult, new()
    {
        /// <summary>
        /// The SDK settings.
        /// </summary>
        protected readonly SaaSApiClientConfiguration clientConfiguration;

        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractSaaSApiRestClient{T}" /> class.
        /// </summary>
        /// <param name="clientConfiguration">The client configuration.</param>
        /// <param name="logger">The logger.</param>
        public AbstractSaaSApiRestClient(SaaSApiClientConfiguration clientConfiguration, ILogger logger)
        {
            this.clientConfiguration = clientConfiguration;
            this.logger = logger;
        }

        /// <summary>
        /// Does the request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>Response deserialized as an instance of T.</returns>
        /// <exception cref="FulfillmentException">Token expired. Please logout and login again.</exception>
        public async Task<T> DoRequest(string url, string method, Dictionary<string, object> parameters, Dictionary<string, object> headers = null, string contentType = "application/json")
        {
            try
            {
                logger?.Info("Call Rest Service : {0}" + JsonSerializer.Serialize(new { url, method, parameters, headers, contentType }));

                var accessTokenResult = await ADAuthenticationHelper.GetAccessToken(clientConfiguration).ConfigureAwait(false);

                if (headers == null)
                {
                    headers = new Dictionary<string, object>();
                }

                // Add bearer token
                headers.Add("Authorization", string.Format($"Bearer {accessTokenResult.AccessToken}"));

                var webRequestHelper = new WebRequestHelper(url, method, contentType);
                await webRequestHelper.PrepareDataForRequest(parameters)
                                        .FillHeaders(headers)
                                        .DoRequestAsync().ConfigureAwait(false);
                return await webRequestHelper.BuildResultFromResponse<T>().ConfigureAwait(false);
            }
            catch (WebException ex)
            {
                logger?.Error(string.Format($"An error occurred while processing request to the url : {url} - {ex.ToString()}"));
                return ProcessErrorResponse(url, ex);
            }
        }

        /// <summary>
        /// Processes the error response.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="ex">The ex.</param>
        /// <returns>Error result built using the data in the response.</returns>
        protected abstract T ProcessErrorResponse(string url, WebException ex);
    }
}