// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.TestFramework;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Marketplace.SaaS;
using Microsoft.Marketplace.SaaS.Models;
using NUnit.Framework;

namespace Microsoft.Marketplace.Tests
{
    [TestFixture]
#pragma warning disable SA1600 // Elements should be documented
    public class FulfillmentTests : RecordedTestBase
#pragma warning restore SA1600 // Elements should be documented
    {
        private IConfigurationRoot config;

        // Changed to record when client code is generated first time manually.
#pragma warning disable SA1600 // Elements should be documented
        public FulfillmentTests()
            : base(true, RecordedTestMode.Playback)
#pragma warning restore SA1600 // Elements should be documented
        {
            this.config = new ConfigurationBuilder()
                .AddUserSecrets<FulfillmentTests>()
                .AddEnvironmentVariables()
                .Build();
        }

        [RecordedTest]
        public async Task GetAllSubscriptionsAsListAsync()
        {
            var sut = this.InstrumentClient(this.GetMarketplaceSaaSClient());
            var subscriptions = await sut.Fulfillment.ListSubscriptionsAsync().ToListAsync();

            Debug.Print($"Received {subscriptions} subscriptions");
            Debug.Print($"Received {subscriptions.Select(s => s.Id).Distinct().ToList().Count} distinct subscriptions");

            NUnit.Framework.Assert.IsTrue(subscriptions.Any());
        }

        [RecordedTest]
        public async Task GetSubscription()
        {
            var sut = this.InstrumentClient(this.GetMarketplaceSaaSClient());
            var subscriptions = await sut.Fulfillment.ListSubscriptionsAsync().ToListAsync();
            Assert.IsTrue(subscriptions.Any());

            var subscription = subscriptions.First();

            var result = await sut.Fulfillment.GetSubscriptionAsync(subscription.Id.Value);

            Assert.IsNotNull(result);
        }

        [RecordedTest]
        public async Task GetAllSubscriptionsPaged()
        {
            var sut = this.InstrumentClient(this.GetMarketplaceSaaSClient());

            var subscriptions = new List<Subscription>();

            string continuationToken = null;
            await foreach (var subscriptionPage in sut.Fulfillment.ListSubscriptionsAsync(continuationToken).AsPages())
            {
                subscriptions.AddRange(subscriptionPage.Values);
                continuationToken = subscriptionPage.ContinuationToken;
            }

            Debug.Print($"Received {subscriptions} subscriptions");
            Debug.Print($"Received {subscriptions.Select(s => s.Id).Distinct().ToList().Count} distinct subscriptions");

            Assert.IsTrue(subscriptions.Any());
        }

        [RecordedTest]

        // [Ignore("Only use with a new marketplace token and record!")]
        public async Task ResolveSubscription()
        {
            var sut = this.InstrumentClient(this.GetMarketplaceSaaSClient());

            // This needs to be run manually after receiving a marketplace token on the landing page, and adding here.
            // Don't forget to urldecode if you are copying from the url param
            var marketplaceToken = "RUA01U9XFK7hkUoCGo0yOCEnXyJHu3cP9VihTQREWTyUkDySoSiMb5j3t3PHXPZUIPN61g1IRQESVIfVRimE+XfdKYiMjg9El3nP0AFhYxuuRMX4jhGeaJHP1JAdz9SP0cti/o6z3RgJDzWTN0eXtLgzbCoRUgdWa64/iHGIFKN30RA9njDxuJkuUp1Ml3wFsQKYcq4HjfD5lUcYOw6amefQ4RzHk9L+krn83OrHfwo=";
            var resolvedSubscription = await sut.Fulfillment.ResolveAsync(marketplaceToken);

            Debug.Print(resolvedSubscription.Value.SubscriptionName);
            Assert.IsNotNull(resolvedSubscription);
        }

        [RecordedTest]
        public async Task UpdateSubscription()
        {
            var sut = this.InstrumentClient(this.GetMarketplaceSaaSClient());

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

        [RecordedTest]
        public async Task OASUpdateFeb22021Subscription()
        {
            var sut = this.InstrumentClient(this.GetMarketplaceSaaSClient());
            var subscriptions = await sut.Fulfillment.ListSubscriptionsAsync().ToListAsync();
            Assert.IsTrue(subscriptions.Any());

            var subscription = subscriptions.First();

            var result = await sut.Fulfillment.GetSubscriptionAsync(subscription.Id.Value);

            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Value.Beneficiary.TenantId);
            Assert.IsInstanceOf<DateTimeOffset>(result.Value.Created);
        }

        [RecordedTest]
        public async Task OASUpdateFeb22021ListAvailablePlans()
        {
            var sut = this.InstrumentClient(this.GetMarketplaceSaaSClient());
            var subscriptions = await sut.Fulfillment.ListSubscriptionsAsync().ToListAsync();

            var contosoSubscription = subscriptions.First(s => s.OfferId == "contoso_saas_offer-preview" && s.SaasSubscriptionStatus == SubscriptionStatusEnum.Subscribed && s.PlanId == "base");

            var availablePlans = await sut.Fulfillment.ListAvailablePlansAsync(contosoSubscription.Id.Value);

            Assert.IsTrue(availablePlans.Value.Plans.SelectMany(p => p.PlanComponents.MeteringDimensions).Any());
            Assert.IsTrue(availablePlans.Value.Plans.SelectMany(p => p.PlanComponents.RecurrentBillingTerms).Any());
        }

        private MarketplaceSaaSClient GetMarketplaceSaaSClient()
        {
            return new MarketplaceSaaSClient(new ClientSecretCredential(this.config["TenantId"], this.config["ClientId"], this.config["clientSecret"]), this.GetOptions());
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
                    Delay = TimeSpan.FromSeconds(this.Mode == RecordedTestMode.Playback ? 0.01 : 1),
                    MaxDelay = TimeSpan.FromSeconds(this.Mode == RecordedTestMode.Playback ? 0.1 : 60),
                    NetworkTimeout = TimeSpan.FromSeconds(this.Mode == RecordedTestMode.Playback ? 100 : 400),
                },
                Transport = new HttpClientTransport(
                new HttpClient()
                {
                    Timeout = TimeSpan.FromSeconds(1000),
                }),
            };
            if (this.Mode != RecordedTestMode.Live)
            {
                options.AddPolicy(new RecordedClientRequestIdPolicy(this.Recording, false), HttpPipelinePosition.PerCall);
            }

            return this.InstrumentClientOptions(options);
        }
    }
}
