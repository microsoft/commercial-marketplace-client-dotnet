#!/bin/bash

git clone https://github.com/Azure/azure-marketplace-openapi.git

npx autorest ./azure-marketplace-openapi/$SDK/autorest.md --v3 --csharp --csharp-sdks-folder=/out  --add-credentials