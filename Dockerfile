# 1. Build application in image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["Src/IdentityService.WebAPI/IdentityService.WebAPI.csproj", "IdentityService.WebAPI/"]
COPY ["Src/IdentityService.Infrastructure/IdentityService.Infrastructure.csproj", "IdentityService.Infrastructure/"]
COPY ["Src/IdentityService.Core/IdentityService.Core.csproj", "IdentityService.Core/"]

RUN dotnet restore "IdentityService.WebAPI/IdentityService.WebAPI.csproj"

COPY ./Src .
WORKDIR "/src/IdentityService.WebAPI"

RUN dotnet build "IdentityService.WebAPI.csproj" -c Release -o /app/build

# 2. Publish built application in image
FROM build AS publish
RUN dotnet publish "IdentityService.WebAPI.csproj" -c Release -o /app/publish

# 3. Take published version
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
EXPOSE 7001
WORKDIR /app
COPY --from=publish /app/publish .
