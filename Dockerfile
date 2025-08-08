FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY src/UserManager.Application/*.csproj ./UserManager.Application/
COPY src/UserManager.Infrastructure/*.csproj ./UserManager.Infrastructure/
COPY src/UserManager.Presentation/*.csproj ./UserManager.Presentation/

RUN dotnet restore

COPY src/. ./
WORKDIR /app/UserManager.Presentation
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/UserManager.Presentation/out .

ENTRYPOINT ["dotnet", "UserManager.Presentation.dll"]
