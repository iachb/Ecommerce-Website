# Fullstack Ecommerce Project (ASP.NET Core + React)

Welcome to my fullstack workshop project: **Building an Ecommerce with ASP.NET Core and React**.

I am building this project from scratch as a professional, real-world ecommerce application using **Clean Architecture** on the backend and **React + Redux Toolkit** on the frontend.

## Project Goal

My goal is to build a complete ecommerce platform while applying modern software engineering practices, including:

- clean and scalable architecture
- secure authentication and authorization
- robust API design with CQRS
- advanced querying and pagination
- external service integrations (payments, email, cloud images)
- modern frontend state management and caching

## What I Am Building

This project is organized as a fullstack solution:

- **Backend:** ASP.NET Core Web API with layered architecture (Domain, Application, Infrastructure, API)
- **Frontend:** React app using Redux Toolkit for state management and cached API operations

## Backend Topics Covered

These are the key backend capabilities I am implementing:

- Layered project structure: Domain | Application | Persistence/Infrastructure | API
- Security model with multiple roles and JWT (JSON Web Tokens)
- EF Core migrations and automated master data seeding in SQL Server
- Mapping between domain entities, ViewModels, and DTOs
- CQRS + MediatR for Controllers and Application features
- Advanced pagination and filtering with Specification Pattern + Entity Framework
- Email delivery with SendGrid/SMTP integration in ASP.NET Core
- Image uploads with cloud services integration
- Stripe payment gateway integration in .NET
- Global exception middleware and standardized client error responses
- Transaction/request validation with FluentValidation
- Additional production-focused best practices

## Frontend Topics Covered

On the frontend, I am using:

- React for building reusable UI components
- Redux Toolkit for application state management
- Slices and caching strategies for API operations
- Component-driven design for scalable UI development

## Technologies

### ASP.NET Core

ASP.NET Core is an open-source, high-performance framework developed by Microsoft and the community. It is modular and cross-platform, making it ideal for modern backend services.

### React

React (React.js) is an open-source JavaScript library for building user interfaces, especially single-page applications. It helps me build interactive, component-based frontend experiences.

### Clean Architecture

Clean Architecture is a set of principles that keeps business logic independent from implementation details. In this project, it helps me keep the domain isolated, testable, and scalable over time.

## What I Am Learning

By building this project, I am learning how to:

- Build an Amazon-style ecommerce clone with React and ASP.NET Core
- Apply Clean Architecture professionally
- Create advanced pagination and query flows with Specification Pattern
- Build a complete ASP.NET Core application using best practices
- Design custom reusable React components
- Implement reactive state management with Redux Toolkit and slices

## Current Repository Structure

```text
backend/
frontend/
```

## Run Backend Locally

From the backend folder:

```bash
dotnet restore
dotnet build
dotnet run --project src/Api/Ecommerce.Api.csproj
```

## Notes

- The backend is actively implemented and follows a layered Clean Architecture approach.
- The frontend folder is prepared for the React application and will continue evolving with the project.

---

This repository documents my end-to-end journey building a production-style ecommerce platform with .NET and React.
