# Microsoft.Marketplace.SaaS

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
title: Microsoft.Marketplace.SaaS
require: $(this-folder)/../autorest.md
input-file:
  - https://github.com/microsoft/commercial-marketplace-openapi/blob/49068a8ec6449fd912588161d9558e8f512c9285/Microsoft.Marketplace.SaaS/2018-08-31/saasapi.v2.json
namespace: Microsoft.Marketplace.SaaS
directive:
  - from: swagger-document
    where: $.definitions.*
    transform: >
      $["x-namespace"] = "Microsoft.Marketplace.SaaS"
  - from: swagger-document
    where: $.components.schemas.TermUnit
    transform: >
      $.enum.push("PAY");
      $["x-ms-enum"] = { name: "TermUnit", modelAsString: true };
# include-csproj: disable
```