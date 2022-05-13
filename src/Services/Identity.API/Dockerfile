#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/NerdStoreEnterprise.Services.Identity.API/NerdStoreEnterprise.Services.Identity.API.csproj", "src/Services/NerdStoreEnterprise.Services.Identity.API/"]
RUN dotnet restore "src/Services/NerdStoreEnterprise.Services.Identity.API/NerdStoreEnterprise.Services.Identity.API.csproj"
COPY . .
WORKDIR "/src/src/Services/NerdStoreEnterprise.Services.Identity.API"
RUN dotnet build "NerdStoreEnterprise.Services.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NerdStoreEnterprise.Services.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NerdStoreEnterprise.Services.Identity.API.dll"]