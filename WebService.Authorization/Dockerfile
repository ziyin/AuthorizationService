FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false
ARG BUILD_CONFIGURATION=Debug

WORKDIR /build
COPY ../ .
WORKDIR /build/WebService.Authorization
RUN dotnet restore

RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish

ARG BUILD_CONFIGURATION=Debug
WORKDIR /build/WebService.Authorization
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebService.Authorization.HttpApi.Host.dll"]
