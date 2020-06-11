docker build -t ercenk/autorest .

docker run --rm --name autorest -v "%cd%"/../sdk:/out -v "%cd%":/input ercenk/autorest  /input/readme.md --v3 --csharp --csharp-sdks-folder=/out  --add-credentials
