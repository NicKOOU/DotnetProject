FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LibraryApp/LibraryApp.csproj", "LibraryApp/"]
RUN dotnet restore "LibraryApp/LibraryApp.csproj"
COPY . .
WORKDIR "/src/LibraryApp"
RUN dotnet build "LibraryApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY ["LibraryApp/setup.sql", "/app/setup.sql"]
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryApp.dll"]