# Ecommerce Platform

> Full-stack e-commerce application built with ASP.NET Core (.NET 7) and React.

![.NET](https://img.shields.io/badge/.NET-7.0-512BD4?logo=dotnet)
![React](https://img.shields.io/badge/React-17-61DAFB?logo=react)
![Redux Toolkit](https://img.shields.io/badge/Redux_Toolkit-latest-764ABC?logo=redux)
![License](https://img.shields.io/badge/license-MIT-green)

---

## Overview

End-to-end e-commerce platform covering the full purchase flow: product catalog with filtering and pagination, shopping cart, user authentication, image management, email-based password recovery, and Stripe payment processing.

The backend follows **Clean Architecture** with a **CQRS/MediatR** pattern. The frontend is a **React SPA** with **Redux Toolkit** for state and API caching.

---

## Repository Structure

```
ecommerce/
├── backend/    # ASP.NET Core Web API (.NET 7)
└── frontend/   # React 17 + Redux Toolkit
```

Each sub-project has its own README with setup instructions and architecture details.

- [Backend →](./backend/README.md)
- [Frontend →](./frontend/README.md)

---

## Tech Stack

| | Backend | Frontend |
|---|---|---|
| Language | C# 11 | TypeScript / JavaScript |
| Framework | ASP.NET Core 7 | React 18 |
| State / Data | EF Core 7 + SQL Server | Redux Toolkit (RTK Query) |
| Auth | ASP.NET Identity + JWT | JWT (stored client-side) |
| Payments | Stripe | Stripe.js |
| Images | Cloudinary | — |
| Email | FluentEmail + SMTP | — |

---

## Quick Start

See the individual READMEs for full setup. Short version:

**Backend**
```bash
cd backend
cp src/Api/appsettings.example.json src/Api/appsettings.json
# fill in your credentials in appsettings.json
dotnet restore && dotnet run --project src/Api/Ecommerce.Api.csproj
```

**Frontend**
```bash
cd frontend
npm install
npm run dev
```

---

## License

MIT
