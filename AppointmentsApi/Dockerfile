FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS http://*:5001
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 5001
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AppointmentsApi/AppointmentsApi.csproj", "AppointmentsApi/"]
RUN dotnet restore "AppointmentsApi/AppointmentsApi.csproj"
COPY . .
WORKDIR "/src/AppointmentsApi"
RUN dotnet build "AppointmentsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppointmentsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppointmentsApi.dll"]
