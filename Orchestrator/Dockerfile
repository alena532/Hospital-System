﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS http://*:5005
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 5005
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Orchestrator/Orchestrator.csproj", "Orchestrator/"]
RUN dotnet restore "Orchestrator/Orchestrator.csproj"
COPY . .
WORKDIR "/src/Orchestrator"
RUN dotnet build "Orchestrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orchestrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orchestrator.dll"]
