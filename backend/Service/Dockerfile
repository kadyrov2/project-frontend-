# Базовый образ для runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["./Service.csproj", "Service/"]
RUN  ls -l /src/Service/

RUN dotnet restore "./Service.csproj"
COPY . .
WORKDIR "/src/Service"

# RUN dotnet build "Service.csproj" --no-cache -c Release -o /app/build

# # Этап публикации
# FROM build AS publish
# RUN dotnet publish "Service.csproj"  --no-cache -c Release -o /app/publish /p:UseAppHost=false

# # Финальный этап
# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Service.dll"]