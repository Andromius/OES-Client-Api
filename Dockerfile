FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY OESAppApi/*.csproj OESAppApi/
COPY Domain/*.csproj Domain/
COPY Persistence/*.csproj Persistence/
RUN dotnet restore OESAppApi/OESAppApi.csproj

# copy and build app and libraries
COPY OESAppApi/ OESAppApi/
COPY Domain/ Domain/
COPY Persistence/ Persistence/

# test stage -- exposes optional entrypoint
# target entrypoint with: docker build --target test
FROM build AS test

COPY tests/*.csproj tests/
WORKDIR /source/tests
RUN dotnet restore

COPY tests/ .
RUN dotnet build --no-restore

ENTRYPOINT ["dotnet", "test", "--logger:trx", "--no-build"]


FROM build AS publish
WORKDIR /source/OESAppApi
RUN dotnet publish --no-restore -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 8080
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OESAppApi.dll"]