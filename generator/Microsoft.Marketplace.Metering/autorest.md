# SaaS

> see https://aka.ms/autorest

This is the AutoRest configuration file for ContainerRegistry.

---

## Getting Started

To build the SDK for ContainerRegistry, simply [Install AutoRest](https://aka.ms/autorest/install) and in this folder, run:

> `autorest`

To see additional help and options, run:

> `autorest --help`

---

## Configuration

### Basic Information

These are the global settings for the ContainerRegistry API.

``` yaml
openapi-type: arm
```

```yaml
input-file:
  - ../2018-08-31/meteringapu.v1.json
```

<!-- ```yaml
input-file:
  - ./marketplace.json
```  -->

# Code Generation

## C#

These settings apply only when `--csharp` is specified on the command line.
Please also specify `--csharp-sdks-folder=<path to "SDKs" directory of your azure-sdk-for-net clone>`.

``` yaml $(csharp)
csharp:
  azure-arm: true
  enable-xml: true
  license-header: MICROSOFT_MIT_NO_VERSION
  namespace: Microsoft.Marketplace.Metering
  payload-flattening-threshold: 3
  public-clients: true
  output-folder: $(csharp-sdks-folder)/src/Microsoft.Marketplace.Metering/Generated
  clear-output-folder: true
```
