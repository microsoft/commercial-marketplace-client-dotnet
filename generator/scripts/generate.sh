#!/bin/bash

git clone https://github.com/Azure/azure-marketplace-openapi.git

npx autorest ./azure-marketplace-openapi/$CLIENT/autorest.md --v3 --csharp --csharp-sdks-folder=/out  --add-credentials
