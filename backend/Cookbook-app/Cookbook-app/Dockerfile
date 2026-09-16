FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# REVIEW(good): USER $APP_UID means the container does not run as root. Keep that.
USER $APP_UID
WORKDIR /app
# REVIEW(azure): EXPOSE 8081 is the HTTPS port from the Visual Studio template and nothing in this app listens on it. Drop it. Azure Container Apps and App Service terminate TLS at the ingress and talk plain HTTP to your container, so 8080 is all you need.
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# REVIEW(good): copying the csproj and restoring before copying the rest of the source is exactly right. It keeps the restore layer cached when only source changes.
COPY ["Cookbook-app/Cookbook-app.csproj", "Cookbook-app/"]
RUN dotnet restore "Cookbook-app/Cookbook-app.csproj"
COPY . .
WORKDIR "/src/Cookbook-app"
RUN dotnet build "./Cookbook-app.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cookbook-app.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# REVIEW(azure): no HEALTHCHECK and no health endpoint in the app. Add a MapHealthChecks("/health") in Program.cs and point the platform probe at it, otherwise Azure routes traffic to a container that is still starting.
# REVIEW(azure): nothing here or in Program.cs applies EF migrations, so a fresh container starts against an empty database and every request 500s. Decide deliberately: either run 'dotnet ef database update' as a separate step in the pipeline (the safer answer for anything with more than one replica) or call db.Database.Migrate() at startup. Right now neither happens.
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cookbook-app.dll"]
