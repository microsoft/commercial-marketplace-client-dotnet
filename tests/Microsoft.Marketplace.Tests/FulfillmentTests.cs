// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.TestFramework;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Marketplace.Metering;
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

        private const string MarketplacePurchaseIdentificationToken = "9EVFKOQwhUCgLB0OKLRdav6AoOXHDKqTUyYgGlJpDy92RdX8XGA+Ebv8XjBcKfcduQW0ggoG2xKszgQozWkU1yUouS+4xsy7xaCSiJRgg6XMkXI9RJV+pdD1/NbSGQIS7gqrxNziYDApXmSRvcd7EuTfdZK3vnsgcWf19g1qpVp+ooAVyCPb8sPvmSuOifJxdvHZu+99YvQURPKnnBchnJK2VQqxbNJZ1e3ZyhYxT28=";

        // Changed to record when client code is generated first time manually.
#pragma warning disable SA1600 // Elements should be documented
        public FulfillmentTests()
            : base(true, RecordedTestMode.Record)
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
        public async Task GetAllSubscriptionsAsListWithCertAsync()
        {
            var sut = this.InstrumentClient(this.GetMarketplaceSaaSClient(true));
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
            var marketplaceToken = MarketplacePurchaseIdentificationToken;
            var resolvedSubscription = await sut.Fulfillment.ResolveAsync(marketplaceToken);

            Debug.Print(resolvedSubscription.Value.SubscriptionName);
            Assert.IsNotNull(resolvedSubscription);
        }

        [RecordedTest]

        // [Ignore("Only use with a new marketplace token and record!")]
        public async Task ResolveSubscriptionWithCert()
        {
            var sut = this.InstrumentClient(this.GetMarketplaceSaaSClient(true));

            // This needs to be run manually after receiving a marketplace token on the landing page, and adding here.
            // Don't forget to urldecode if you are copying from the url param
            var marketplaceToken = MarketplacePurchaseIdentificationToken;
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

            var operation = await sut.Operations.GetOperationStatusAsync(firstActiveSubscription.Id.Value, operationId);

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

        [RecordedTest]
        public async Task PostSingleUsage()
        {
            var sut = this.InstrumentClient(this.GetMarketplaceMeteringClient());

            var usageEvent = new Metering.Models.UsageEvent { 
                ResourceUri = "/subscriptions/bf7adf12-c3a8-426c-9976-29f145eba70f/resourceGroups/ercmngd/providers/Microsoft.Solutions/applications/ercuserassigned",
                Quantity = 15,
                Dimension = "dim1",
                EffectiveStartTime = DateTime.Now.AddHours(-65),
                PlanId = "userassigned",
            };

            var result = await sut.Metering.PostUsageEventAsync(usageEvent);

        }

        private MarketplaceSaaSClient GetMarketplaceSaaSClient(bool useCert = false)
        {
            TokenCredential creds;
            if (useCert)
            {
                var password = this.config["certPassword"];
                var filePath = this.config["certFilePath"];

                var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

                var certCollection = new X509Certificate2Collection();
                certCollection.Import(filePath, password, X509KeyStorageFlags.PersistKeySet);

                var cert = certCollection[0];

                creds = new ClientCertificateCredential(this.config["TenantId"], this.config["ClientId"], cert);
            }
            else
            {
                creds = new ClientSecretCredential(this.config["TenantId"], this.config["ClientId"], this.config["clientSecret"]);
            }

            return new MarketplaceSaaSClient(creds, this.GetMarketplaceSaaSClientOptions());
        }

        private MarketplaceMeteringClient GetMarketplaceMeteringClient(bool useCert = false)
        {
            TokenCredential creds;
            if (useCert)
            {
                var password = this.config["certPassword"];
                var filePath = this.config["certFilePath"];

                var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

                var certCollection = new X509Certificate2Collection();
                certCollection.Import(filePath, password, X509KeyStorageFlags.PersistKeySet);

                var cert = certCollection[0];

                creds = new ClientCertificateCredential(this.config["TenantId"], this.config["ClientId"], cert);
            }
            else
            {
                creds = new ClientSecretCredential(this.config["TenantId"], this.config["ClientId"], this.config["clientSecret"]);
            }

            return new MarketplaceMeteringClient(creds, this.GetMarketplaceMeteringClientOptions());
        }

        private MarketplaceSaaSClientOptions GetMarketplaceSaaSClientOptions()
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

        private MarketplaceMeteringClientOptions GetMarketplaceMeteringClientOptions()
        {
            var options = new MarketplaceMeteringClientOptions()
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
