FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lumix.API/Lumix.API.csproj", "Lumix.API/"]
COPY ["Lumix.Application/Lumix.Application.csproj", "Lumix.Application/"]
COPY ["Lumix.Core/Lumix.Core.csproj", "Lumix.Core/"]
COPY ["Lumix.Infrastructure/Lumix.Infrastructure.csproj", "Lumix.Infrastructure/"]
COPY ["Lumix.Persistence/Lumix.Persistence.csproj", "Lumix.Persistence/"]
RUN dotnet restore "Lumix.API/Lumix.API.csproj"
COPY . .
WORKDIR "/src/Lumix.API"
RUN dotnet build "Lumix.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lumix.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lumix.API.dll"]
