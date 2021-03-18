# Microsoft.Marketplace.SaaS

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
title: Microsoft.Marketplace.SaaS
require: $(this-folder)/../autorest.md
input-file:
  - https://github.com/microsoft/commercial-marketplace-openapi/blob/94e2eb217f684c7b2c15834f01ee6a5e521a6c49/Microsoft.Marketplace.SaaS/2018-08-31/saasapi.v2.json
namespace: Microsoft.Marketplace.SaaS
directive:
  from: swagger-document
  where: $.definitions.*
  transform: >
    $["x-namespace"] = "Microsoft.Marketplace.SaaS"
# include-csproj: disable
```