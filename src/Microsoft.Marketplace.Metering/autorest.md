# Microsoft.Marketplace.Metering

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
title: Microsoft.Marketplace.Metering
require: $(this-folder)/../autorest.md
input-file:
  - https://github.com/microsoft/commercial-marketplace-openapi/blob/ae3f6900997e0bf850dae86aec61098cf179efd3/Microsoft.Marketplace.Metering/2018-08-31/meteringapi.v1.json
namespace: Microsoft.Marketplace.Metering
directive:
  from: swagger-document
  where: $.definitions.*
  transform: >
    $["x-namespace"] = "Microsoft.Marketplace.Metering"
```
