# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY api.kapidunyasi.sln ./
COPY global.json ./
COPY src/Api.KapiDunyasi.Domain/Api.KapiDunyasi.Domain.csproj ./src/Api.KapiDunyasi.Domain/
COPY src/Api.KapiDunyasi.Application/Api.KapiDunyasi.Application.csproj ./src/Api.KapiDunyasi.Application/
COPY src/Api.KapiDunyasi.Infrastructure/Api.KapiDunyasi.Infrastructure.csproj ./src/Api.KapiDunyasi.Infrastructure/
COPY src/Api.KapiDunyasi.WebAPI/Api.KapiDunyasi.WebAPI.csproj ./src/Api.KapiDunyasi.WebAPI/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build and publish the application
WORKDIR /src/src/Api.KapiDunyasi.WebAPI
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Create a non-root user for security
RUN groupadd -r appuser && useradd -r -g appuser appuser

# Copy published files from build stage
COPY --from=build /app/publish .

# Set ownership to non-root user
RUN chown -R appuser:appuser /app

# Switch to non-root user
USER appuser

# Expose port (Coolify will map this)
EXPOSE 8080

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

# Start the application
ENTRYPOINT ["dotnet", "Api.KapiDunyasi.WebAPI.dll"]
