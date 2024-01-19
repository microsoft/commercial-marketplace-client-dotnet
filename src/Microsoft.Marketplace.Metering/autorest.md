# Microsoft.Marketplace.Metering

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
title: Microsoft.Marketplace.Metering
require: $(this-folder)/../autorest.md
input-file:
  - https://github.com/microsoft/commercial-marketplace-openapi/blob/4eede61a53c509ea78e4ad36591bea793aafa44c/Microsoft.Marketplace.Metering/2018-08-31/meteringapi.v1.json
namespace: Microsoft.Marketplace.Metering
directive:
  from: swagger-document
  where: $.definitions.*
  transform: >
    $["x-namespace"] = "Microsoft.Marketplace.Metering"
```
