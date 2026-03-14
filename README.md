# FleetOps Support Hub

FleetOps Support Hub is a portfolio-ready **C# ASP.NET Core Web API** built in **VS Code** to demonstrate backend development skills relevant to full-stack .NET roles.

The application simulates an internal operations platform for a custom software company that manages **client accounts**, **escalated support tickets**, and **scheduled software releases**. It is designed to reflect real business workflows rather than a toy project.

## Why this project

This project demonstrates:

- ASP.NET Core Web API development
- Relational data modeling with **Entity Framework Core** and **SQLite**
- CRUD operations with filtering and status-based workflows
- RESTful endpoint design
- Support and escalation tracking logic
- Release management workflows
- Swagger / OpenAPI documentation
- Clean GitHub-ready project structure
- Development in **VS Code**

## Tech Stack

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- VS Code

## Core Entities

### ClientAccount
Represents a client organization using the software platform.

### SupportTicket
Represents escalated issues, investigation work, and ticket status tracking.

### ReleaseItem
Represents scheduled software releases, optionally linked to support tickets.

## Features

- Manage client accounts
- Create and update support tickets
- Track ticket priority and status
- Schedule and complete release items
- Filter data through API query parameters
- Test endpoints through Swagger UI

## API Routes

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

## Run Locally

### Requirements
- .NET 8 SDK
- VS Code or Visual Studio

### Steps
```bash
git clone https://github.com/Kyla-Zeit/fleetops-support-hub.git
cd fleetops-support-hub
dotnet restore
dotnet run

Then open Swagger UI in your browser at:

http://localhost:5184/swagger
