# Microsoft.Marketplace.Metering

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
title: Microsoft.Marketplace.Metering
require: $(this-folder)/../autorest.md
input-file:
  - https://github.com/microsoft/commercial-marketplace-openapi/blob/c817ffae1642f09937633f7b41090891958d8500/Microsoft.Marketplace.Metering/2018-08-31/meteringapi.v1.json
namespace: Microsoft.Marketplace.Metering
directive:
  from: swagger-document
  where: $.definitions.*
  transform: >
    $["x-namespace"] = "Microsoft.Marketplace.Metering"
```
