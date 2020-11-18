docker build -t marketplacedotnet/autorest .


docker run --rm --name autorest -v "%cd%"/scripts:/scripts -v "%cd%"/../client:/out -e CLIENT=Microsoft.Marketplace.SaaS marketplacedotnet/autorest  

docker run --rm --name autorest -v "%cd%"/scripts:/scripts -v "%cd%"/../client:/out -e CLIENT=Microsoft.Marketplace.Metering marketplacedotnet/autorest 
