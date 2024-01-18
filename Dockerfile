# Build
#FROM bitnami/dotnet-sdk:8 AS build
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /source
COPY . .
#RUN dotnet restore "./OESAppApi/OESAppApi.csproj" --disable-parallel -p:EnableSdkContainerSupport=false  ma byt dole --no-restore
RUN dotnet publish "./OESAppApi/OESAppApi.csproj" -a linux-arm64 -c release -o /app 

# Serve
#FROM bitnami/aspnet-core:7
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80

ENTRYPOINT ["dotnet", "OESAppApi.dll"]
