FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["BookTracker.Api/BookTracker.Api.csproj", "BookTracker.Api/"]
RUN dotnet restore "BookTracker.Api/BookTracker.Api.csproj"
COPY . .
WORKDIR "/src/BookTracker.Api"
RUN dotnet build "BookTracker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookTracker.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookTracker.Api.dll"]
