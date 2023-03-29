# Microsoft.Marketplace.SaaS

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
title: Microsoft.Marketplace.SaaS
require: $(this-folder)/../autorest.md
input-file:
  - https://github.com/microsoft/commercial-marketplace-openapi/blob/0c7ce552cb6d5121b983f8351697965b6551f8cd/Microsoft.Marketplace.SaaS/2018-08-31/saasapi.v2.json
namespace: Microsoft.Marketplace.SaaS
directive:
  from: swagger-document
  where: $.definitions.*
  transform: >
    $["x-namespace"] = "Microsoft.Marketplace.SaaS"
# include-csproj: disable
```