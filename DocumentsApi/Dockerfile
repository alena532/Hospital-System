FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS http://*:5003
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 5003
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DocumentsApi/DocumentsApi.csproj", "DocumentsApi/"]
RUN dotnet restore "DocumentsApi/DocumentsApi.csproj"
COPY . .
WORKDIR "/src/DocumentsApi"
RUN dotnet build "DocumentsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocumentsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentsApi.dll"]
