docker build -t microsoft/autorest .


docker run --rm --name autorest -v "%cd%"/../sdk:/out -e SDK=Microsoft.Marketplace.SaaS microsoft/autorest  

docker run --rm --name autorest -v "%cd%"/../sdk:/out -e SDK=Microsoft.Marketplace.Metering microsoft/autorest 
