FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS http://*:5006
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 5006
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProfilesApi/ProfilesApi.csproj", "ProfilesApi/"]
RUN dotnet restore "ProfilesApi/ProfilesApi.csproj"
COPY . .
WORKDIR "/src/ProfilesApi"
RUN dotnet build "ProfilesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfilesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfilesApi.dll"]
