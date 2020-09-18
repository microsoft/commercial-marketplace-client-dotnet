docker build -t microsoft/autorest .

docker run --rm --name autorest -v "%cd%"/../sdk1:/out -v "%cd%"/../Microsoft.Marketplace.SaaS:/input microsoft/autorest  --output-folder=/out

docker run --rm --name autorest -v "%cd%"/../sdk1:/out -v "%cd%"/../Microsoft.Marketplace.Metering:/input microsoft/autorest  --output-folder=/out

