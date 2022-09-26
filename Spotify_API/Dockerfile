#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Spotify_API/Spotify_API.csproj", "Spotify_API/"]
RUN dotnet restore "Spotify_API/Spotify_API.csproj"
COPY . .
WORKDIR "/src/Spotify_API"
RUN dotnet build "Spotify_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Spotify_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spotify_API.dll"]