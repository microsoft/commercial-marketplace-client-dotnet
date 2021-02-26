# Microsoft.Marketplace.Metering

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
title: Microsoft.Marketplace.Metering
require: $(this-folder)/../autorest.md
input-file:
  - https://github.com/microsoft/commercial-marketplace-openapi/blob/197383f3c35df9e4683f1a2919d6c14edf3ce979/Microsoft.Marketplace.Metering/2018-08-31/meteringapi.v1.json
namespace: Microsoft.Marketplace.Metering
# include-csproj: disable
directive:
  from: swagger-document
  where: $.definitions.*
  transform: >
    $["x-namespace"] = "Microsoft.Marketplace.Metering"
```