# Microsoft.Marketplace.SaaS

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
title: Microsoft.Marketplace.SaaS
require: $(this-folder)/../autorest.md
input-file:
  - https://github.com/microsoft/commercial-marketplace-openapi/blob/6ca8208abac8ad6d72b0ff899749aa3e458951d3/Microsoft.Marketplace.SaaS/2018-08-31/saasapi.v2.json
namespace: Microsoft.Marketplace.SaaS
directive:
  from: swagger-document
  where: $.definitions.*
  transform: >
    $["x-namespace"] = "Microsoft.Marketplace.SaaS"
# include-csproj: disable
```