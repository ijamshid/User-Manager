# 1. Runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# 2. SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 3. Copy only .csproj files and restore
COPY ["src/UserManager.Presentation/UserManager.Presentation.csproj", "src/UserManager.Presentation/"]
COPY ["src/UserManager.Application/UserManager.Application.csproj", "src/UserManager.Application/"]
COPY ["src/UserManager.Infrastucture/UserManager.Infrastucture.csproj", "src/UserManager.Infrastucture/"]
COPY ["src/UserManager.Core/UserManager.Core.csproj", "src/UserManager.Core/"]
RUN dotnet restore "src/UserManager.Presentation/UserManager.Presentation.csproj"

# 4. Copy all other files
COPY . .

# 5. Build the application
WORKDIR "/src/src/UserManager.Presentation"
RUN dotnet build "UserManager.Presentation.csproj" -c Release -o /app/build

# 6. Publish to /app/publish
FROM build AS publish
RUN dotnet publish "UserManager.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 7. Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManager.Presentation.dll"]
