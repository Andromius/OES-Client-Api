start dotnet publish --os linux --arch x64
timeout /t 5
start dotnet publish --os linux --arch arm64 -p:ContainerImageTags=arm64