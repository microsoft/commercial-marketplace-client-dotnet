# Microsoft.Marketplace.Metering

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
title: Microsoft.Marketplace.Metering
require: $(this-folder)/../autorest.md
input-file:
  - https://github.com/Azure/commercial-marketplace-openapi/blob/d0fb20989ff75c2fe3862acada93ba71855c14e7/Microsoft.Marketplace.Metering/2018-08-31/meteringapi.v1.json
namespace: Microsoft.Marketplace.Metering
# include-csproj: disable
directive:
  from: swagger-document
  where: $.definitions.*
  transform: >
    $["x-namespace"] = "Microsoft.Marketplace.Metering"
```