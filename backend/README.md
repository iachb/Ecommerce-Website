# Ecommerce Backend

> REST API for a full-stack e-commerce application built with ASP.NET Core (.NET 7), Clean Architecture, and CQRS.

![.NET](https://img.shields.io/badge/.NET-7.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-11-239120?logo=csharp)
![EF Core](https://img.shields.io/badge/EF_Core-7-blue)
![License](https://img.shields.io/badge/license-MIT-green)

---

## Table of Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Database](#database)
- [API Reference](#api-reference)

---

## Overview

This backend exposes a versioned REST API (`/api/v1/`) for a complete e-commerce flow: product catalog, shopping cart, user auth, image uploads, email-based password recovery, and Stripe payment processing.

The solution follows **Clean Architecture** with a **DDD-inspired** domain model and **CQRS via MediatR**, keeping business logic strictly out of the API layer.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Runtime | .NET 7 / C# 11 |
| Web framework | ASP.NET Core Web API |
| ORM | Entity Framework Core 7 (SQL Server) |
| Auth | ASP.NET Identity + JWT Bearer |
| CQRS / Mediator | MediatR |
| Validation | FluentValidation |
| Mapping | AutoMapper |
| Payments | Stripe (Payment Intents) |
| Images | Cloudinary |
| Email | FluentEmail + MailKit (SMTP) |
| API docs | Swagger / OpenAPI (Swashbuckle) |

---

## Architecture

```
backend/
└── src/
    ├── Api/                        # Controllers, middleware, DI configuration
    ├── Core/
    │   ├── Ecommerce.Domain/       # Entities, base models (no dependencies)
    │   └── Ecommerce.Application/  # CQRS handlers, DTOs, validators, interfaces
    └── Infrastructure/             # EF DbContext, repositories, external services
```

**Dependency rule:** `Api → Application → Domain` — Infrastructure implements Application interfaces and is never referenced by Domain or Application directly.

### Key patterns

- **CQRS** — Commands mutate state; Queries return data. Handlers live in `Application/Features/*/Commands` and `Application/Features/*/Queries`.
- **Repository + Unit of Work** — `IUnitOfWork.Repository<T>()` with a single `Complete()` commit.
- **Specification pattern** — Encapsulates EF Core queries (filters, sorting, paging, includes) as reusable objects.
- **MediatR pipeline behaviors** — `ValidationBehaviour` runs FluentValidation before every handler; `UnhandledExceptionBehaviour` catches and logs unhandled exceptions.
- **ExceptionMiddleware** — Converts exceptions to consistent JSON error responses with appropriate HTTP status codes.

---

## Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- SQL Server (local or remote)
- Stripe account (for payments)
- Cloudinary account (for image uploads)
- SMTP credentials (for email)

### Run locally

```bash
# From the backend/ directory

# 1. Copy the example config and fill in your credentials
cp src/Api/appsettings.example.json src/Api/appsettings.json

# 2. Restore, build, and run
dotnet restore
dotnet build
dotnet run --project src/Api/Ecommerce.Api.csproj
```

Swagger UI is available at `https://localhost:{port}/swagger` in the `Development` environment.

---

## Configuration

Copy `appsettings.json` and fill in the required values. The app reads configuration from `appsettings.json` and `appsettings.Development.json`.

```jsonc
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=EcommerceDb;..."
  },
  "JwtSettings": {
    "Key": "<secret>",
    "Issuer": "<issuer>",
    "Audience": "<audience>",
    "DurationInMinutes": 60
  },
  "StripeSettings": {
    "PublishableKey": "pk_test_...",
    "SecretKey": "sk_test_..."
  },
  "CloudinarySettings": {
    "CloudName": "...",
    "ApiKey": "...",
    "ApiSecret": "..."
  },
  "EmailFluentSettings": {
    "Host": "smtp.example.com",
    "Port": 587,
    "UserName": "...",
    "Password": "...",
    "BaseUrlClient": "https://your-frontend-url.com"
  }
}
```

---

## Database

Migrations live in `src/Infrastructure/Migrations/`.

```bash
# Add a new migration
dotnet ef migrations add <MigrationName> \
  -p src/Infrastructure/Ecommerce.Infrastructure.csproj \
  -s src/Api/Ecommerce.Api.csproj \
  -c EcommerceDbContext

# Apply migrations
dotnet ef database update \
  -p src/Infrastructure/Ecommerce.Infrastructure.csproj \
  -s src/Api/Ecommerce.Api.csproj \
  -c EcommerceDbContext
```

In the `Development` environment the API automatically runs `MigrateAsync()` and seeds initial data on startup via `EcommerceDbContextData.LoadDataAsync`.

---

## API Reference

All routes are prefixed with `/api/v1/`. Full interactive docs are available via Swagger at runtime.

| Resource | Endpoints |
|---|---|
| **Auth / Users** | `POST /login`, `POST /register`, password recovery, role management |
| **Products** | `GET` list + detail with pagination, `POST/PUT/DELETE` (admin), image upload |
| **Shopping Cart** | `GET` cart, `PUT` update items, `DELETE` remove items |
| **Payments** | `POST` create Stripe Payment Intent |

---

## License

MIT
