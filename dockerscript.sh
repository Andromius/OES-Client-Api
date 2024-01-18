#!/bin/bash

TARGETARCH=$1

if [ "$TARGETARCH" == "amd64" ]; then
    dotnet publish "OESAppApi.csproj" -c Release -o /app/publish -r linux-x64 --sc
elif [ "$TARGETARCH" == "arm64" ]; then
    dotnet publish "OESAppApi.csproj" -c Release -o /app/publish -r linux-arm64 --sc
fi