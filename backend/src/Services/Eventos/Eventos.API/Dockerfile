FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/Events/Events.API/Events.API.csproj", "src/Services/Events/Events.API/"]
COPY ["src/Services/Events/Events.Application/Events.Application.csproj", "src/Services/Events/Events.Application/"]
COPY ["src/Services/Events/Events.Domain/Events.Domain.csproj", "src/Services/Events/Events.Domain/"]
COPY ["src/Services/Events/Events.Infrastructure/Events.Infrastructure.csproj", "src/Services/Events/Events.Infrastructure/"]
COPY ["src/Shared/BuildingBlocks/Application/BuildingBlocks.Application.csproj", "src/Shared/BuildingBlocks/Application/"]
COPY ["src/Shared/BuildingBlocks/Domain/BuildingBlocks.Domain.csproj", "src/Shared/BuildingBlocks/Domain/"]
RUN dotnet restore "src/Services/Events/Events.API/Events.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Events/Events.API"
RUN dotnet build "Events.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Events.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Events.API.dll"]
