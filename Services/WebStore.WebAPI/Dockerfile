#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/WebStore.WebAPI/WebStore.WebAPI.csproj", "Services/WebStore.WebAPI/"]
COPY ["Data/WebStore.DAL/WebStore.DAL.csproj", "Data/WebStore.DAL/"]
COPY ["Common/WebStore.Domain/WebStore.Domain.csproj", "Common/WebStore.Domain/"]
COPY ["Services/WebStore.Services/WebStore.Services.csproj", "Services/WebStore.Services/"]
COPY ["Services/WebStore.Interfaces/WebStore.Interfaces.csproj", "Services/WebStore.Interfaces/"]
COPY ["Common/WebStore.Logging/WebStore.Logging.csproj", "Common/WebStore.Logging/"]
COPY ["Data/WebStore.DAL.Sqlite/WebStore.DAL.Sqlite.csproj", "Data/WebStore.DAL.Sqlite/"]
RUN dotnet restore "Services/WebStore.WebAPI/WebStore.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/WebStore.WebAPI"
RUN dotnet build "WebStore.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebStore.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebStore.WebAPI.dll"]