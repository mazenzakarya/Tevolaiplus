# Use the official .NET 10 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy source code per project
COPY ["Tevolai.BL/", "Tevolai.BL/"]
COPY ["Tevolai.DAL/", "Tevolai.DAL/"]
COPY ["Tevolai.APIs/", "Tevolai.APIs/"]


RUN dotnet restore "Tevolai.APIs/Tevolai.APIs.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Tevolai.APIs"
RUN dotnet build "Tevolai.APIs.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "Tevolai.APIs.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Copy published app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Tevolai.APIs.dll"]

