// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Marketplace.Tests
{
    extern alias AzureIdentity;
    extern alias AzureCore;
    extern alias AzureCoreTestFramework;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using AzureCore::Azure.Core.Pipeline;
    using AzureCore::Azure.Core;
    using AzureCoreTestFramework::Azure.Core.TestFramework;
    using AzureIdentity::Azure.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Marketplace.SaaS;
    using Microsoft.Marketplace.SaaS.Models;
    using NUnit.Framework;
    using System.Net.Http;

    [TestFixture]
    public class FulfillmentTests : RecordedTestBase
    {
        private IConfigurationRoot config;

        // Changed to record when client code is generated first time manually.
        public FulfillmentTests() : base(true, RecordedTestMode.Playback)
        {
            this.config = new ConfigurationBuilder()
                .AddUserSecrets<FulfillmentTests>()
                .AddEnvironmentVariables()
                .Build();
        }

        [RecordedTest]
        public async Task GetAllSubscriptionsAsListAsync()
        {
            var sut = InstrumentClient(GetMarketplaceSaaSClient());
            var subscriptions = await sut.Fulfillment.ListSubscriptionsAsync().ToListAsync();

            Debug.Print($"Received {subscriptions} subscriptions");
            Debug.Print($"Received {subscriptions.Select(s => s.Id).Distinct().ToList().Count} distinct subscriptions");

            NUnit.Framework.Assert.IsTrue(subscriptions.Any());
        }

        [RecordedTest]
        public async Task GetSubscription()
        {
            var sut = InstrumentClient(GetMarketplaceSaaSClient());
            var subscriptions = await sut.Fulfillment.ListSubscriptionsAsync().ToListAsync();
            Assert.IsTrue(subscriptions.Any());

            var subscription = subscriptions.First();

            var result = await sut.Fulfillment.GetSubscriptionAsync(subscription.Id.Value);

            Assert.IsNotNull(result);
        }

        [RecordedTest]
        public async Task GetAllSubscriptionsPaged()
        {
            var sut = InstrumentClient(GetMarketplaceSaaSClient());

            var subscriptions = new List<Subscription>();

            string continuationToken = null;
            await foreach (AzureCore.Azure.Page<Subscription> subscriptionPage in sut.Fulfillment.ListSubscriptionsAsync(continuationToken).AsPages())
            {
                subscriptions.AddRange(subscriptionPage.Values);
                continuationToken = subscriptionPage.ContinuationToken;
            }

            Debug.Print($"Received {subscriptions} subscriptions");
            Debug.Print($"Received {subscriptions.Select(s => s.Id).Distinct().ToList().Count} distinct subscriptions");

            Assert.IsTrue(subscriptions.Any());
        }

        [RecordedTest]
        //[Ignore("Only use with a new marketplace token and record!")]
        public async Task ResolveSubscription()
        {
            var sut = InstrumentClient(GetMarketplaceSaaSClient());

            // This needs to be run manually after receiving a marketplace token on the landing page, and adding here.
            // Don't forget to urldecode if you are copying from the url param
            var marketplaceToken = "9j3fH3/V5rznrU9e2OC4aqQ+NRfWfR8ICNPNgMvlHsk9CKYJOM6Zsx+vxWxr+0DWCmSdX6I1OfD0UrM3D6lfzwdu9J3CLlTenZkU48ZDsOirCOFO30VT/jffXcrtm5nfJYc6UnCUWZPElxb2XiU1E7rBrh6XpX+Izo0r9Fc40U2gIoOi8P04VKeGxPh3RqDyPxNk2xPO+BV07rYC3Fpsxq+I0R9CKLUgrhrmcvGf76hjT1rFAYPaGGZsvFS4lL1M";
            var resolvedSubscription = await sut.Fulfillment.ResolveAsync(marketplaceToken);

            Debug.Print(resolvedSubscription.Value.SubscriptionName);
            Assert.IsNotNull(resolvedSubscription);
        }

        [RecordedTest]
        public async Task UpdateSubscription()
        {
            var sut = InstrumentClient(GetMarketplaceSaaSClient());

            var subscriptions = await sut.Fulfillment.ListSubscriptionsAsync().ToListAsync();
            Assert.IsTrue(subscriptions.Any());

            var firstActiveSubscription = subscriptions.First(s => s.SaasSubscriptionStatus.Value == SubscriptionStatusEnum.Subscribed);

            var plan = firstActiveSubscription.PlanId == "silver" ? "gold" : "silver";

            var result = await sut.Fulfillment.UpdateSubscriptionAsync(firstActiveSubscription.Id.Value, new SubscriberPlan() { PlanId = plan });

            // Cannot check whether this succeeed or not.
            var operationId = Guid.TryParse(result, out var value) ? value : Guid.Empty;

            var operation = await sut.SubscriptionOperations.GetOperationStatusAsync(firstActiveSubscription.Id.Value, operationId);

            Assert.IsNotNull(operation);

            Debug.Print(Enum.GetName(typeof(OperationStatusEnum), operation.Value.Status));
        }

        private MarketplaceSaaSClient GetMarketplaceSaaSClient()
        {
            return new MarketplaceSaaSClient(new ClientSecretCredential(config["TenantId"], config["ClientId"], config["clientSecret"]), GetOptions());
        }

        private MarketplaceSaaSClientOptions GetOptions()
        {
            var options = new MarketplaceSaaSClientOptions()
            {
                Diagnostics = { IsLoggingEnabled = true },
                Retry =
                {
                    Mode = RetryMode.Exponential,
                    MaxRetries = 10,
                    Delay = TimeSpan.FromSeconds(Mode == RecordedTestMode.Playback ? 0.01 : 1),
                    MaxDelay = TimeSpan.FromSeconds(Mode == RecordedTestMode.Playback ? 0.1 : 60),
                    NetworkTimeout = TimeSpan.FromSeconds(Mode == RecordedTestMode.Playback ? 100 : 400),
                },
                Transport = new HttpClientTransport(
                new HttpClient()
                {
                    Timeout = TimeSpan.FromSeconds(1000)
                })
            };
            if (Mode != RecordedTestMode.Live)
            {
                options.AddPolicy(new RecordedClientRequestIdPolicy(Recording, false), HttpPipelinePosition.PerCall);
            }

            return InstrumentClientOptions(options);
        }
    }
}
