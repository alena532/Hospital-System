FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["_GateWay/_GateWay.csproj", "_GateWay/"]
RUN dotnet restore "_GateWay/_GateWay.csproj"
COPY . .
WORKDIR "/src/_GateWay"
RUN dotnet build "_GateWay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "_GateWay.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "_GateWay.dll"]
