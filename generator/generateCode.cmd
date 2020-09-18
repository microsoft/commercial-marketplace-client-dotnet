docker build -t microsoft/autorest .


docker run --rm --name autorest -v "%cd%"/../sdk:/out -v "%cd%"/Microsoft.Marketplace.SaaS:/input microsoft/autorest

docker run -it --rm --name autorest -v "%cd%"/../sdk:/out -v "%cd%"/Microsoft.Marketplace.Metering:/input microsoft/autorest

