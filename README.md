![.NET Core](https://github.com/Azure/commercial-marketplace-saas-client-dotnet/workflows/.NET%20Core/badge.svg)

# Using the Client Library

The library has two clients, one for the [SaaS fulfillment API version 2](https://docs.microsoft.com/en-us/azure/marketplace/partner-center-portal/pc-saas-fulfillment-api-v2) (**MarketplaceSaaSClient**) and one for [metered billing APIs](https://docs.microsoft.com/en-us/azure/marketplace/partner-center-portal/marketplace-metering-service-apis) (**MarketplaceMeteringClient**).

Both of them use the client credentials flow, service-to-service authentication for authenticating to call the APIs. The clients support both client secrets and X.509 certificates.After registering an single-tenant application on Azure AD, just create a client secret, or use a X.509 to call the constructor as follows:
``` csharp
    new MarketplaceSaaSClient(<tenantId>, <clientId>, <clientSecret>);
```

Then call the appropriate method corresponding to the API call.
``` csharp
    var page = await this.sut.FulfillmentOperations.ListSubscriptionsAsync();
```

Most of the code is generated, except an extension method

- ListAllSubscriptionsAsync - List all of the subscriptions, implementing pagination.

# Running the tests
The tests use the standard .NET core configuration system user secrets for configuration. Simply add user secrets for the following values, using your favorite method, i.e. either ["dotnet user-secrets"](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows), Visual Studio Code extension, or Visual Studio 2019 user secrets menu option.

- TenantId
- ClientId
- clientSecret

One test requires a marketplace purchase identification token and you can get it from a landing page link generated from an actual subscription. Simply url-decode it, and set the value in the test.


# Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
