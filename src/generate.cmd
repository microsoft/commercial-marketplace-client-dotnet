cd Microsoft.Marketplace.SaaS
@REM dotnet build /t:GenerateCode -p:AutoRestInput="Microsoft.Marketplace.SaaS.md"
dotnet build /t:GenerateCode 

cd ..\Microsoft.Marketplace.Metering
@REM dotnet build /t:GenerateCode -p:AutoRestInput="Microsoft.Marketplace.Metering.md"
dotnet build /t:GenerateCode 