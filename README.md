# FleetOps Support Hub

This project simulates a small internal operations platform for a custom software company that supports fleet-tracking clients, manages escalated issues, and tracks release items scheduled for deployment.

- ASP.NET Core Web API development
- Relational data modeling with **Entity Framework Core + SQLite**
- CRUD endpoints with filtering and status workflows
- Support/escalation domain logic
- Release management concepts
- Swagger documentation
- GitHub-friendly repo structure
- Development in **VS Code**

## Tech stack

- C#
- .NET 8
- ASP.NET Core
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- VS Code

## Domain model

### ClientAccount
Represents a client using the software platform.

### SupportTicket
Represents escalated support issues, investigation work, and ticket status.

### ReleaseItem
Represents scheduled releases, linked to tickets where applicable.

## API routes

### Clients
- `GET /api/clients`
- `GET /api/clients/{id}`
- `POST /api/clients`
- `PUT /api/clients/{id}`
- `DELETE /api/clients/{id}`

### Tickets
- `GET /api/tickets`
- `GET /api/tickets?status=Escalated`
- `GET /api/tickets/{id}`
- `POST /api/tickets`
- `PUT /api/tickets/{id}`
- `PATCH /api/tickets/{id}/resolve`
- `DELETE /api/tickets/{id}`

### Releases
- `GET /api/releases`
- `GET /api/releases?sundayNightOnly=true`
- `GET /api/releases/{id}`
- `POST /api/releases`
- `PUT /api/releases/{id}`
- `PATCH /api/releases/{id}/complete`
- `DELETE /api/releases/{id}`

## Getting started in VS Code

### 1. Create the project folder locally
Unzip this project and open the folder in VS Code.

### 2. Install prerequisites
- .NET 8 SDK
- VS Code
- C# extension (recommended automatically)

### 3. Restore packages
```bash
dotnet restore
```

### 4. Run the API
```bash
dotnet run
```

### 5. Open Swagger
Open the local URL shown in the terminal, then go to `/swagger` if it does not open automatically.

## Sample JSON for testing

### Create a client
```json
{
  "name": "CityFleet Dispatch",
  "primaryContact": "Jordan Smith",
  "email": "jordan@cityfleet.example",
  "isActive": true
}
```

### Create a ticket
```json
{
  "title": "Geo-fence alerts delayed by 90 seconds",
  "description": "Alert events arrive late during high-volume route updates.",
  "status": "Escalated",
  "priority": "High",
  "assignedTo": "Rebecca Maguire",
  "clientAccountId": 1
}
```

### Create a release
```json
{
  "version": "2026.3.2",
  "summary": "Fix for delayed geo-fence processing and queue optimization.",
  "scheduledUtc": "2026-03-15T21:00:00Z",
  "sundayNightRelease": true,
  "status": "Scheduled",
  "supportTicketId": 1
}
```
