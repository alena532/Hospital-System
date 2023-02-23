FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS http://*:5002
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 5002
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AuthApi/AuthApi.csproj", "AuthApi/"]
RUN dotnet restore "AuthApi/AuthApi.csproj"
COPY . .
WORKDIR "/src/AuthApi"
RUN dotnet build "AuthApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthApi.dll"]
