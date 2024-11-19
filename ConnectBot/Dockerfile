# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY ConnectBot/*.csproj ./ConnectBot/
WORKDIR /app/ConnectBot
RUN dotnet restore

# Copy the remaining project files and publish the app
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose port 80
EXPOSE 8080

# Set the environment variable for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:8080

# Run the application
ENTRYPOINT ["dotnet", "ConnectBot.dll"]