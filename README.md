![.NET Core](https://github.com/microsoft/commercial-marketplace-client-dotnet/workflows/.NET%20Core/badge.svg)

# Change log
    - Started using AutoRest C# generator V3 to take advantage of new features.
    - Removed container based code generation. Using the code build target for code generation coming with C# generator V3. Please see the generate.cmd file in the src folder.
    - Modified the repo structure for streamlining with the new code generation method.
    - Added two C# projects supporting code generation (SaaS and Metering) and one project for building the binary assets (Microsoft.Marketplace.csproj).    

## Breaking changes for preview2

    - Constructors for the clients are now using classes deriving from Azure.Core.TokenCredential defined in Azure.Identity namespace (in Azure.Identity NuGet package)
    - Removed *TokenProvider 
    - Removed ListAllSubscriptionsAsync method on the FulfillmentOperations in favor of the generated ListSubscriptions* methods that implement pagination. Please see the tests for different usages.
    - Renamed FulfillmentOperations property on the MarketplaceSaaSClient to Fulfillment
    - Removed serialization checks on the SubscriberPlan and UsageEvent classes for consistency. The libraries rely on serialization checks on the service side.

# Using the client libraries

The library has two clients, one for the [SaaS fulfillment API version 2](https://docs.microsoft.com/en-us/azure/marketplace/partner-center-portal/pc-saas-fulfillment-api-v2) (**MarketplaceSaaSClient**) and one for [metered billing APIs](https://docs.microsoft.com/en-us/azure/marketplace/partner-center-portal/marketplace-metering-service-apis) (**MarketplaceMeteringClient**).

Both of them use the client credentials flow, service-to-service authentication for authenticating to call the APIs. The clients support both client secrets and X.509 certificates.After registering an single-tenant application on Azure AD, just create a client secret, or use a X.509 to call the constructor as follows:
``` csharp
    new MarketplaceSaaSClient(new ClientSecretCredential(config["TenantId"], config["ClientId"], config["clientSecret"]));
```

If you want to use an X.509 certificate instead, use *ClientCertificateCredential* where clientCertificate is of type System*.Security.Cryptography.X509Certificates.X509Certificate2.
``` csharp
        new MarketplaceSaaSClient(new ClientCertificateCredential (config["TenantId"], config["ClientId"], clientCertificate);
```

Then call the appropriate method corresponding to the API call.
``` csharp
    var subscriptions = await this.sut.Fulfillment.ListSubscriptionsAsync().ToListAsync();
```

# Running the tests
We are using the Azure.Core.TestFramework from the [Azure SDK for .NET](https://github.com/Azure/azure-sdk-for-net/tree/master/sdk/core/Azure.Core.TestFramework). Since no NuGet package includes this assembly at the time of the development of this library project, we opted to make a copy of that source tree and directly include in our repository.

We will be turning on the RecordedTestMode.Record each time we generate the client code when the API surface changes or we need to change the implementation. We are including the session recordings, and the tests will be run against the recorded sessions by default.

The tests use the standard .NET core configuration system user secrets for configuration. Simply add user secrets for the following values, using your favorite method, i.e. either ["dotnet user-secrets"](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows), Visual Studio Code extension, or Visual Studio 2019 user secrets menu option.

- TenantId
- ClientId
- clientSecret

If you want to record your own sessions, turn on recording by modifying the test constructor and use RecordedTestMode.Record, then run the tests.

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
