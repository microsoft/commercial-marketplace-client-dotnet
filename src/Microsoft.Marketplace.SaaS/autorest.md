# Microsoft.Marketplace.SaaS

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
title: Microsoft.Marketplace.SaaS
require: $(this-folder)/../autorest.md
input-file:
  - https://github.com/Azure/commercial-marketplace-openapi/blob/d0fb20989ff75c2fe3862acada93ba71855c14e7/Microsoft.Marketplace.SaaS/2018-08-31/saasapi.v2.json
namespace: Microsoft.Marketplace.SaaS
directive:
  from: swagger-document
  where: $.definitions.*
  transform: >
    $["x-namespace"] = "Microsoft.Marketplace.SaaS"
# include-csproj: disable
```