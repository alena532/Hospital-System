﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS http://*:5004
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 5004
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OfficesApi/OfficesApi.csproj", "OfficesApi/"]
RUN dotnet restore "OfficesApi/OfficesApi.csproj"
COPY . .
WORKDIR "/src/OfficesApi"
RUN dotnet build "OfficesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OfficesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OfficesApi.dll"]
