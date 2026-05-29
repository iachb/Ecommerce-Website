# Ecommerce Backend (ASP.NET Core / .NET 7)

Backend API for an e‑commerce application built with **ASP.NET Core (.NET 7)** following **Clean Architecture** and a **DDD (Domain‑Driven Design)**-inspired approach. The project uses **CQRS with MediatR**, **Entity Framework Core** for persistence, and a set of infrastructure services (auth, email, payments, images) decoupled from the domain.

> Backend folder: `backend/`  
> Layered solution: `src/Api`, `src/Core`, `src/Infrastructure`

---

## Stack / Technologies

### Platform
- **.NET 7** / **C# 11**
- **ASP.NET Core Web API**
- **Swagger / OpenAPI** (Swashbuckle)

### Architecture & patterns
- **Clean Architecture** (Api → Application → Domain → Infrastructure)
- **CQRS** with **MediatR** (Commands / Queries)
- **Unit of Work + Repository** (generic)
- **Specification pattern** (paging, filters, sorting and includes)
- **AutoMapper** (Domain ⇄ ViewModels / DTOs mapping)
- **FluentValidation** (validation through the MediatR pipeline)

### Security
- **ASP.NET Identity** (usuarios/roles)
- **JWT Bearer Authentication**
- **Role-based authorization** + explicit anonymous endpoints (`[AllowAnonymous]`)

### Integrations
- **Stripe** (Payment Intents)
- **Cloudinary** (image management)
- **FluentEmail + SMTP (MailKit)** (email sending, password recovery)

### Persistence
- **Entity Framework Core 7**
- Database currently configured for **SQL Server** (provider `UseSqlServer`).

---

## Project structure

```
backend/
  src/
	Api/                    # Entry layer: controllers, middleware, configuration
	Core/
	  Ecommerce.Domain/      # Domain entities and domain model
	  Ecommerce.Application/ # CQRS, VMs/DTOs, validation, contracts, behaviors
	Infrastructure/          # EF DbContext, repositories, UnitOfWork, external services
```

### Layers

#### `src/Api` (Presentation)
- REST controllers: `ProductController`, `UserController`, `ShoppingCartController`, `PaymentController`, etc.
- **Swagger** and **JWT** configuration.
- **ExceptionMiddleware** to handle exceptions and return consistent responses.

#### `src/Core/Ecommerce.Application` (Application)
- Features organized as vertical slices (use cases):
  - `Features/*/Commands/*` and `Features/*/Queries/*`
- MediatR **behaviors**:
  - `UnhandledExceptionBehaviour` (exception handling)
  - `ValidationBehaviour` (FluentValidation execution)
- Centralized **MappingProfile** (AutoMapper).
- Infrastructure contracts (interfaces): `IAuthService`, `IEmailService`, `IManageImageService`, etc.

#### `src/Core/Ecommerce.Domain` (Domain)
- Business entities: `Product`, `Order`, `OrderItem`, `ShoppingCart`, `Address`, etc.
- Base model `BaseDomainModel` (audit fields: created/modified).

#### `src/Infrastructure` (Infrastructure)
- `EcommerceDbContext` (EF Core + IdentityDbContext)
- `RepositoryBase<T>` + `UnitOfWork`
- External implementations:
  - Email (`EmailService`)
  - Cloudinary (`ManageImageService`)
- `Migrations/` (EF Core migrations)
- Seed / initial data (`EcommerceDbContextData.LoadDataAsync`)

---

## Key features

### 1) CQRS + MediatR
Endpoints (Controllers) delegate logic to **handlers** (`IRequestHandler`) and avoid business logic in the API layer.

- **Commands**: state-changing operations (create/update/delete).
- **Queries**: reads (lists, detail, pagination).

### 2) Repository + Unit of Work
- `IUnitOfWork.Repository<TEntity>()` returns a generic repository.
- Allows grouping operations and committing with `Complete()`.

### 3) Specification pattern
In infrastructure, specifications are evaluated for:
- criteria (filters)
- sorting
- paging
- includes (`Include`) with `AsSplitQuery` and `AsNoTracking`.

This is mainly used for lists and pagination.

### 4) Validation & error handling
- **FluentValidation** validates requests before handlers run.
- **ExceptionMiddleware** formats errors and HTTP status codes (400/401/404/500).

### 5) AuthN/AuthZ with Identity + JWT
- Identity manages users/roles.
- JWT protects endpoints.
- A global authorization filter is applied and selected endpoints are opened with `[AllowAnonymous]`.

### 6) Images (Cloudinary)
- Image uploads from `multipart/form-data` endpoints.
- `ManageImageService` encapsulates the Cloudinary integration.

### 7) Emails (password recovery)
- `FluentEmail` + SMTP sending.
- Reset URL built using `EmailFluentSettings.BaseUrlClient`.

### 8) Payments (Stripe)
- Create/update `PaymentIntent`.
- `PaymentIntentId` and `ClientSecret` are stored in the `Order` entity.

---
## Running the project

Requirements:
- SDK **.NET 7**
- An instance of the DB (configured in `appsettings.json` connection string, currently SQL Server).

From `backend/`:

```bash
dotnet restore
dotnet build
dotnet run --project src/Api/Ecommerce.Api.csproj
```

Swagger:
- In development, open `/swagger` (see `launchSettings.json`).

---

## Migrations & database (EF Core)

Migrations live in: `src/Infrastructure/Migrations`.

Typical commands (same pattern used in this repo):

```bash
dotnet ef migrations add <NombreMigracion> \
  -p src/Infrastructure/Ecommerce.Infrastructure.csproj \
  -s src/Api/Ecommerce.Api.csproj \
  -c EcommerceDbContext

dotnet ef database update \
  -p src/Infrastructure/Ecommerce.Infrastructure.csproj \
  -s src/Api/Ecommerce.Api.csproj \
  -c EcommerceDbContext
```

In `Development`, the API automatically runs:
- `context.Database.MigrateAsync()`
- `EcommerceDbContextData.LoadDataAsync(...)` (seed)

---

## Endpoints (high-level)

The API is versioned under `api/v1/[controller]`.

Examples (see `src/Api/Controllers`):
- **Users**: login/register, password recovery, roles, admin pagination.
- **Products**: list, detail, pagination, CRUD (admin), images.
- **ShoppingCart**: get cart, update, delete items.
- **Payments**: create payment (Stripe).

Swagger is generated with an `All` document and also one per controller (configured in `Program.cs`).

---