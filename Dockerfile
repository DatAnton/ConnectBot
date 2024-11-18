# Base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

# Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["ConnectBot/ConnectBot.csproj", "ConnectBot/"]
RUN dotnet restore "ConnectBot/ConnectBot.csproj"

# Copy the rest of the source code
COPY . .

# Set the working directory for build
WORKDIR "/src/ConnectBot"
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final stage: runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConnectBot.dll"]