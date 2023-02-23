FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS http://*:5007
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 5007
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ServicesApi/ServicesApi.csproj", "ServicesApi/"]
RUN dotnet restore "ServicesApi/ServicesApi.csproj"
COPY . .
WORKDIR "/src/ServicesApi"
RUN dotnet build "ServicesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServicesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServicesApi.dll"]
