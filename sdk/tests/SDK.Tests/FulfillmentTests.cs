// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using Microsoft.Extensions.Configuration;
using Microsoft.Marketplace.SaaS;
using Microsoft.Marketplace.SaaS.Models;
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
        private MarketplaceSaaSClient sut;

        public FulfillmentTests()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets<FulfillmentTests>()
                .AddEnvironmentVariables()
                .Build();

            this.sut = new MarketplaceSaaSClient(Guid.Parse(config["TenantId"]), Guid.Parse(config["ClientId"]), config["clientSecret"]);
        }


        [TestMethod]
        public async Task GetAllSubscriptionsWithNext()
        {
            var subscriptions = new List<Subscription>();

            var page = await this.sut.FulfillmentOperations.ListSubscriptionsAsync();
            subscriptions.AddRange(page);

            while (page.NextPageLink != default)
            {
                page = await this.sut.FulfillmentOperations.ListSubscriptionsNextAsync(page.NextPageLink);
                subscriptions.AddRange(page);
            }

            Debug.Print($"Received {subscriptions.Count} subscriptions");
            Debug.Print($"Received {subscriptions.Select(s => s.Id).Distinct().ToList().Count()} distinct subscriptions");

            var allSubscriptions = await this.sut.FulfillmentOperations.ListAllSubscriptionsAsync();
            
            Assert.IsTrue(subscriptions.Any());
            Assert.AreEqual(allSubscriptions.Count(), subscriptions.Count);
        }

        [TestMethod]
        public async Task GetSubscription()
        {
            var subscriptions = this.sut.FulfillmentOperations.ListSubscriptions();
            Assert.IsTrue(subscriptions.Any());

            var subscription = subscriptions.First();

            var result = await this.sut.FulfillmentOperations.GetSubscriptionAsync(subscription.Id.Value);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Ignore]
        public async Task ResolveSubscription()
        {
            // This needs to be run manually after receiving a marketplace token on the landing page, and adding here.
            // Don't forget to urldecode if you are copying from the url param
            var marketplaceToken = "LWW36TzssfHGvUNuDpSy9hVq9RZxrompe1bZT+qRRzqbwx83uxHyRVmYXjjqaYs550FQ07JAu27Suk5YqlnkAoBKt6makiced8hbTFpBS3WRda8Y0KoDTAl9fb0LEkFbpJ1MrcRglQrZLkrmc63ZlLcTQhrq/TNmvVcx4tOlq2f+RcvOdbzPrj7J3bScBNX/1Ug70NXrRMlENawNNORdTZ34eSBaJ9YnBiFcJ7oxcU4=";
            var resolvedSubscription = await this.sut.FulfillmentOperations.ResolveAsync(marketplaceToken);

            Debug.Print(resolvedSubscription.SubscriptionName);    
            Assert.IsNotNull(resolvedSubscription);
        }

        [TestMethod]
        public async Task UpdateSubscription()
        {
            var subscriptions = await this.sut.FulfillmentOperations.ListSubscriptionsAsync();
            Assert.IsTrue(subscriptions.Any());

            var firstActiveSubscription = subscriptions.First(s => s.SaasSubscriptionStatus.Value == SubscriptionStatusEnum.Subscribed);

            var plan = firstActiveSubscription.PlanId == "silver" ? "gold" : "silver";

            var result = await this.sut.FulfillmentOperations.UpdateSubscriptionAsync(firstActiveSubscription.Id.Value, null, null, planId: plan);
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
            var subscriptions = await this.sut.FulfillmentOperations.ListSubscriptionsAsync();
            Assert.IsTrue(subscriptions.Any());

            var firstActiveSubscription = subscriptions.First(s => s.SaasSubscriptionStatus.Value == SubscriptionStatusEnum.Subscribed);

            var plan = firstActiveSubscription.PlanId == "silver" ? "gold" : "silver";
            var quantity = 5;

            var result = await this.sut.FulfillmentOperations.UpdateSubscriptionAsync(firstActiveSubscription.Id.Value, null, null, planId: plan, quantity);

            Assert.Fail("Should never come here");
        }
    }
}
