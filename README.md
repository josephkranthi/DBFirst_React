# DBFirst_React

This repository contains a full-stack sample application built with ASP.NET Core and React. It demonstrates a simple employee management workflow where a SQL Server database stores employee records and a React frontend interacts with them through a Web API.

## Overview

The project is split into two main parts:

- `DBFirst_React.Server` — ASP.NET Core Web API backend
- `dbfirst_react.client` — React + Vite frontend

The backend uses Entity Framework Core and SQL Server, while the frontend uses a Vite-based React app and communicates with the API through the `/api` route.

## Tech stack

### Backend

- C# and .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server

### Frontend

- React
- JavaScript
- Vite
- CSS and HTML

### Testing

- MSTest
- NUnit
- xUnit

## Architecture

- Backend API: `DBFirst_React.Server`
  - `Program.cs` configures the ASP.NET app, OpenAPI, and SQL Server connection
  - `Controllers/EmployeeController.cs` exposes CRUD operations for employees
  - `Models/Employee.cs` defines the employee entity
  - `Models/EmployeeDbContext.cs` configures the EF Core database context

- Frontend app: `dbfirst_react.client`
  - `src/App.jsx` renders the employee list and form
  - `vite.config.js` configures the React app and proxies `/api` requests to the backend

## Features

- Fetch all employees from the API
- Create a new employee
- Delete an employee
- Display employee data in a simple table UI
- ASP.NET SPA proxy setup for local development

## Prerequisites

Before running the app, make sure you have:

- .NET SDK 10
- Node.js and npm
- SQL Server instance running locally
- A database named `EmployeeDB` available on `localhost`

## Configuration

The backend connection string is defined in `DBFirst_React.Server/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Update this value if your SQL Server instance uses a different server, database, or authentication setup.

## Running the application

From the repository root:

```bash
dotnet restore
npm install --prefix dbfirst_react.client
dotnet run --project DBFirst_React.Server
```

This starts the ASP.NET backend and the React SPA proxy for the frontend. The React app is configured to run on the development server proxied by the backend.

If you want to run the frontend alone for local Vite development:

```bash
cd dbfirst_react.client
npm install
npm run dev
```

## Project structure

```text
DBFirst_React/
├── DBFirst_React.Server/
│   ├── Controllers/
│   ├── Models/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── DBFirst_React.Server.csproj
│   └── ...
├── dbfirst_react.client/
│   ├── src/
│   ├── package.json
│   ├── vite.config.js
│   └── ...
├── MSTest/
├── NUnit/
├── xUnit/
├── DBFirst_React.slnx
├── README.md
└── .gitignore
```

## Testing

The repository includes several test projects:

- `MSTest`
- `NUnit`
- `xUnit`

Run the test suite with:

```bash
dotnet test DBFirst_React.slnx
```

## Notes

This project is a practical example of a database-first ASP.NET Core backend paired with a React frontend. It is useful as a starter for learning CRUD patterns, EF Core configuration, and frontend development.
