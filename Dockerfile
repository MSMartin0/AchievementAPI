#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AchievementAPI.csproj", "."]
RUN dotnet restore "./AchievementAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AchievementAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AchievementAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY Resources ./Resources
ENTRYPOINT ["dotnet", "AchievementAPI.dll"]