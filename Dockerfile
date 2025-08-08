FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY User-Manager.sln ./

COPY src/UserManager.Application/UserManager.Application.csproj src/UserManager.Application/
COPY src/UserManager.Infrastructure/UserManager.Infrastructure.csproj src/UserManager.Infrastructure/
COPY src/UserManager.Presentation/UserManager.Presentation.csproj src/UserManager.Presentation/
COPY src/UserManager.Domain/UserManager.Domain.csproj src/UserManager.Domain/

RUN dotnet restore

COPY src/. ./src

WORKDIR /app/src/UserManager.Presentation
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "UserManager.Presentation.dll"]
