# CSCE-361

Trello board: https://trello.com/invite/361projectorganization/ATTI70287d4977fb69b3401d3778333b9397F1D06C44

# E-Commerce Store

This is a full-stack e-commerce web application built with:

- ASP.NET Core (C#) for the backend
- React + Vite for the frontend
- SQL Server running in Docker
- EF Core for database access and migrations

---

## Features

- User registration and login
- Product browsing
- Persistent SQL Server database using Docker
- Clean architecture with DTOs and services

---

## Requirements

Ensure you have the following installed:

- [Docker](https://www.docker.com/)
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js (v18+)](https://nodejs.org/) and npm

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/Datquangbui1011/CSCE-361.git
cd e-commerce-store-service-host
```

### 2. Install Frontend Dependencies

```bash
cd e-commerce-store-service-host.client
npm install
cd ..
```

### 3. Start SQL Server with Docker

```bash
cd e-commerce-store-service-host.Server
docker compose down -v
docker compose up -d
```
### 4. Apply EF Core Migrations

```bash
dotnet ef database update
```

### 5. Run the Project

```bash
dotnet run
```
