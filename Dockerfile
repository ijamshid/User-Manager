# 1. SDK bilan build qilish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copy csproj va dependencies
COPY *.sln .
COPY UserManager.Application/*.csproj ./UserManager.Application/
COPY UserManager.Infrastructure/*.csproj ./UserManager.Infrastructure/
COPY UserManager.Presentation/*.csproj ./UserManager.Presentation/

RUN dotnet restore

# copy everything else and build
COPY . .
WORKDIR /app/UserManager.Presentation
RUN dotnet publish -c Release -o out

# 2. Run-time image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/UserManager.Presentation/out .

ENTRYPOINT ["dotnet", "UserManager.Presentation.dll"]
