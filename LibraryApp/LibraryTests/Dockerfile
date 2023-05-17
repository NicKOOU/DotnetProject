FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LibraryTests/LibraryTests.csproj", "LibraryTests/"]
RUN dotnet restore "LibraryTests/LibraryTests.csproj"
COPY . .
WORKDIR "/src/LibraryTests"
RUN dotnet build "LibraryTests.csproj" -c Release -o /app/build

FROM build AS testrunner
WORKDIR /src
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "test", "--logger:trx"]
