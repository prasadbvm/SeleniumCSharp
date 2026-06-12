FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and restore as distinct layers
COPY *.slnx ./
COPY AutomotiveBddProject/*.csproj AutomotiveBddProject/
RUN dotnet restore "AutomotiveBddProject.slnx"

# Copy everything else and publish
COPY . .
RUN dotnet publish "AutomotiveBddProject.slnx" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "AutomotiveBddProject.dll"]
