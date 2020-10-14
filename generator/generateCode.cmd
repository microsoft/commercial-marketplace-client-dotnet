docker build -t marketplacedotnet/autorest .


docker run --rm --name autorest -v "%cd%"/scripts:/scripts -v "%cd%"/../sdk:/out -e SDK=Microsoft.Marketplace.SaaS marketplacedotnet/autorest  

docker run --rm --name autorest -v "%cd%"/scripts:/scripts -v "%cd%"/../sdk:/out -e SDK=Microsoft.Marketplace.Metering marketplacedotnet/autorest 
