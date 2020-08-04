// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using Microsoft.Extensions.Configuration;
using Microsoft.Marketplace;
using Microsoft.Marketplace.Models;
using Microsoft.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SDK.Tests
{
    [TestClass]
    public class FulfillmentTests
    {
        private MarketplaceClient sut;

        public FulfillmentTests()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets<FulfillmentTests>()
                .AddEnvironmentVariables()
                .Build();

            this.sut = new MarketplaceClient(Guid.Parse(config["TenantId"]), Guid.Parse(config["ClientId"]), config["clientSecret"]);
        }


        [TestMethod]
        public async Task GetAllSubscriptionsWithNext()
        {
            var subscriptions = new List<Subscription>();

            var page = await this.sut.Fulfillment.ListSubscriptionsAsync();
            subscriptions.AddRange(page);

            while (page.NextPageLink != default(string))
            {
                page = await this.sut.Fulfillment.ListSubscriptionsNextAsync(page.NextPageLink);
                subscriptions.AddRange(page);
            }

            Debug.Print($"Received {subscriptions.Count} subscriptions");
            Debug.Print($"Received {subscriptions.Select(s => s.Id).Distinct().ToList().Count()} distinct subscriptions");

            var allSubscriptions = await this.sut.Fulfillment.ListAllSubscriptionsAsync();
            
            Assert.IsTrue(subscriptions.Any());
            Assert.AreEqual(allSubscriptions.Count(), subscriptions.Count);
        }

        [TestMethod]
        public async Task GetSubscription()
        {
            var subscriptions = this.sut.Fulfillment.ListSubscriptions();
            Assert.IsTrue(subscriptions.Any());

            var subscription = subscriptions.First();

            var result = await this.sut.Fulfillment.GetSubscriptionAsync(subscription.Id.Value);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Ignore]
        public async Task ResolveSubscription()
        {
            // This needs to be run manually after receiving a marketplace token on the landing page, and adding here.
            // Don't forget to urldecode if you are copying from the url param
            var marketplaceToken = "1sjcR8VVNx2W3oM0wfAfaj0D1yJQIRCeoaDNm7qzQzCXiMGAnuDXnLp+JNPR2e74tgddbEsMqsxKWn23+WubOvNT3FM3aTfGcQTlPe/PPnTQBK6ltqUZSTIKRUjEes+qdZOQSkO4o9k5pF9yio97+s18Dy8MXTawXwhvjF8vdTsjSUfHtzKIpodRWPROYiOgDTQR9pgDhGHNtCU+caxmUlPb5wux5KPBe67RrDXjonE=";
            var resolvedSubscription = await this.sut.Fulfillment.ResolveAsync(marketplaceToken);

            Debug.Print(resolvedSubscription.SubscriptionName);    
            Assert.IsNotNull(resolvedSubscription);
        }

        [TestMethod]
        public async Task UpdateSubscription()
        {
            var subscriptions = await this.sut.Fulfillment.ListSubscriptionsAsync();
            Assert.IsTrue(subscriptions.Any());

            var firstActiveSubscription = subscriptions.First(s => s.SaasSubscriptionStatus.Value == SubscriptionStatusEnum.Subscribed);

            var plan = firstActiveSubscription.PlanId == "silver" ? "gold" : "silver";

            var result = await this.sut.Fulfillment.UpdateSubscriptionAsync(firstActiveSubscription.Id.Value, null, null, planId: plan);
            // Cannot check whether this succeeed or not, 
            var operationId = result.OperationLocation;

            var operation = await this.sut.SubscriptionOperations.GetOperationStatusAsync(firstActiveSubscription.Id.Value, operationId);

            Assert.IsNotNull(operation);

            Debug.Print(Enum.GetName(typeof(OperationStatusEnum), operation.Status));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public async Task ThrowsWhenBothPlanIdandQuantityPresent()
        {
            var subscriptions = await this.sut.Fulfillment.ListSubscriptionsAsync();
            Assert.IsTrue(subscriptions.Any());

            var firstActiveSubscription = subscriptions.First(s => s.SaasSubscriptionStatus.Value == SubscriptionStatusEnum.Subscribed);

            var plan = firstActiveSubscription.PlanId == "silver" ? "gold" : "silver";
            var quantity = 5;

            var result = await this.sut.Fulfillment.UpdateSubscriptionAsync(firstActiveSubscription.Id.Value, null, null, planId: plan, quantity);

            Assert.Fail("Should never come here");
        }
    }
}
