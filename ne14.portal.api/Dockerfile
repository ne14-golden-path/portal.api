#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ne14.portal.business/ne14.portal.business.csproj", "ne14.portal.business/"]
COPY ["ne14.portal.api/ne14.portal.api.csproj", "ne14.portal.api/"]
RUN --mount=type=secret,id=nuget_config_file dotnet restore "./ne14.portal.api/./ne14.portal.api.csproj" --configfile "/run/secrets/nuget_config_file"
COPY . .
WORKDIR "/src/ne14.portal.api"
RUN dotnet build "./ne14.portal.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ne14.portal.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ne14.portal.api.dll"]