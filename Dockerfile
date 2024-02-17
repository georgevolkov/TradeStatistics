FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 5095

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TradeStatisticsAPI/TradeStatisticsAPI.csproj", "TradeStatisticsAPI/"]
RUN dotnet restore "./TradeStatisticsAPI/./TradeStatisticsAPI.csproj"
COPY . .
WORKDIR "/src/TradeStatisticsAPI"
RUN dotnet build "./TradeStatisticsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TradeStatisticsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TradeStatisticsAPI.dll"]