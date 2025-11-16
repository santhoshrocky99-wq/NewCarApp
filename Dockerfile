# Use official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything first so that restore sees all files
COPY . .

# Restore and publish
RUN dotnet restore "CarCleanz.csproj"
RUN dotnet publish "CarCleanz.csproj" -c Release -o /app/publish

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarCleanz.dll"]