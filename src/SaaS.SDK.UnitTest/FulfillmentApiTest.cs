namespace Microsoft.Marketplace.Client.UnitTest
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Marketplace.Client.Configurations;
    using Microsoft.Marketplace.Client.Helpers;
    using Microsoft.Marketplace.Client.Models;
    using Microsoft.Marketplace.Client.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// FulfillmentApi Test Class.
    /// </summary>
    [TestClass]
    public class FulfillmentApiTest
    {
        /// <summary>
        /// The client.
        /// </summary>
        private FulfillmentApiClient client;

        /// <summary>
        /// The configuration.
        /// </summary>
        private SaaSApiClientConfiguration configuration = new SaaSApiClientConfiguration();

        /// <summary>
        /// Initializes a new instance of the <see cref="FulfillmentApiTest" /> class.
        /// </summary>
        public FulfillmentApiTest()
        {
            var builder = new ConfigurationBuilder();

            IConfigurationRoot config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
               .Build();

            this.configuration = config.GetSection("AppSetting").Get<SaaSApiClientConfiguration>();
            this.client = new FulfillmentApiClient(this.configuration, null);
        }

        /// <summary>
        /// Checks the authentication.
        /// </summary>
        /// <returns> Check Authentication.</returns>
        [TestMethod]
        public async Task CheckAuthentication()
        {
            var accessTokenResult = await ADAuthenticationHelper.GetAccessToken(this.configuration).ConfigureAwait(false);
            Assert.IsNotNull(accessTokenResult);
            Assert.IsNotNull(accessTokenResult?.AccessToken);
        }

        /// <summary>
        /// Gets the subscription by identifier.
        /// </summary>
        /// <returns>Test Subscription By Identifier.</returns>
        [TestMethod]
        public async Task GetSubscriptionByID()
        {
            var allSubscriptions = await this.client.GetAllSubscriptionAsync().ConfigureAwait(false);
            var subscriptionId = allSubscriptions.FirstOrDefault().Id;
            var subscriptionDetail = await this.client.GetSubscriptionByIdAsync(subscriptionId);
            Assert.IsNotNull(subscriptionDetail);
            Assert.AreEqual(subscriptionId, subscriptionDetail?.Id);
        }

        /// <summary>
        /// Gets the subscription by identifier exception.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [ExpectedException(typeof(FulfillmentException), "Subscription Not Found")]
        [TestMethod]
        public async Task GetSubscriptionByIDException()
        {
            var subscriptionId = Guid.NewGuid();
            _ = await this.client.GetSubscriptionByIdAsync(subscriptionId);
        }
    }
}
