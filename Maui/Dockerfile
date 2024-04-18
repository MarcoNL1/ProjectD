FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Maui.csproj", "./"]
RUN dotnet restore "MauiApp1/Maui.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Maui.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS development
WORKDIR /src
CMD dotnet run --no-launch-profile

FROM build AS publish
ARG BUILD_CONFIGURATION
RUN dotnet publish "Maui.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maui.dll"]
