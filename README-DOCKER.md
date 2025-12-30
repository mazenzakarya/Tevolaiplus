# Docker Setup for Tevolai Project

## Overview
This project uses Docker Compose to run:
- **.NET 10 API** on port **5000** (HTTP) and **5001** (HTTPS)
- **SQL Server 2022** on port **1433**
- **Angular Frontend** on port **4200** (when ready)

## Prerequisites
- Docker Desktop installed
- Docker Compose v3.8+

## Quick Start

### 1. Build and Start Services
```bash
docker-compose up -d --build
```

### 2. Access Services
- **API**: http://localhost:5000
- **Swagger UI**: http://localhost:5000 (in Development mode)
- **SQL Server**: localhost:1433
- **Frontend**: http://localhost:4200 (when Angular is added)

### 3. Stop Services
```bash
docker-compose down
```

### 4. Stop and Remove Volumes (Clean Slate)
```bash
docker-compose down -v
```

## Services Configuration

### API Service (.NET 10)
- **Port**: 5000 (HTTP), 5001 (HTTPS)
- **Environment**: Development (change in docker-compose.yml for production)
- **Database**: Automatically connects to SQL Server container
- **CORS**: Configured for Angular frontend on localhost:4200

### SQL Server
- **Port**: 1433
- **SA Password**: `YourStrong@Passw0rd` (change in production!)
- **Database**: TevolaiDb
- **Data Persistence**: Uses Docker volume `sqlserver_data`

### Frontend (Angular) - Optional
To enable Angular frontend:
1. Create Angular project in `./frontend` directory
2. Uncomment the `frontend` service in `docker-compose.yml`
3. Rebuild: `docker-compose up -d --build`

## Environment Variables

### API Environment Variables
- `ASPNETCORE_ENVIRONMENT`: Development/Production
- `ASPNETCORE_URLS`: http://+:5000
- `ConnectionStrings__DefaultConnection`: SQL Server connection string
- `Jwt__Key`: JWT secret key
- `EncryptionKey`: Encryption key for sensitive data

### SQL Server Environment Variables
- `ACCEPT_EULA`: Y
- `SA_PASSWORD`: YourStrong@Passw0rd (change in production!)
- `MSSQL_PID`: Express

## Production Deployment

### Security Checklist
1. ✅ Change `SA_PASSWORD` to a strong password
2. ✅ Set `ASPNETCORE_ENVIRONMENT=Production`
3. ✅ Use environment-specific connection strings
4. ✅ Use secure JWT keys (from environment variables or secrets)
5. ✅ Configure proper CORS origins (remove localhost in production)
6. ✅ Use HTTPS only
7. ✅ Configure firewall rules

### Example Production docker-compose.yml
```yaml
services:
  api:
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ASPNETCORE_URLS=http://+:5000
      - ConnectionStrings__DefaultConnection=${DB_CONNECTION_STRING}
      - Jwt__Key=${JWT_SECRET}
      - EncryptionKey=${ENCRYPTION_KEY}
```

## Troubleshooting

### API not starting
- Check logs: `docker-compose logs api`
- Verify SQL Server is healthy: `docker-compose ps`
- Check port 5000 is not in use

### Database connection issues
- Verify SQL Server container is running: `docker-compose ps sqlserver`
- Check connection string in docker-compose.yml
- Wait for health check to pass (10-30 seconds)

### Frontend not connecting to API
- Verify CORS configuration in Program.cs
- Check API_URL environment variable
- Ensure both services are on the same network

## Network Architecture
```
┌─────────────┐
│  Frontend   │ (Angular - Port 4200)
│  (nginx)    │
└──────┬──────┘
       │ HTTP
       ▼
┌─────────────┐
│     API     │ (.NET 10 - Port 5000)
│  (Kestrel)  │
└──────┬──────┘
       │ SQL
       ▼
┌─────────────┐
│ SQL Server  │ (Port 1433)
└─────────────┘
```

## Useful Commands

```bash
# View logs
docker-compose logs -f api
docker-compose logs -f sqlserver

# Rebuild specific service
docker-compose up -d --build api

# Execute commands in container
docker-compose exec api dotnet --version
docker-compose exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd

# Clean everything
docker-compose down -v
docker system prune -a
```



