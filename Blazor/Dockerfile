FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Blazor.csproj", "./"]
RUN dotnet restore "Blazor.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS development
WORKDIR /src
EXPOSE 80
EXPOSE 443
CMD dotnet watch --no-launch-profile --non-interactive --no-hot-reload

FROM build AS publish
ARG BUILD_CONFIGURATION
RUN dotnet publish "Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blazor.dll"]
