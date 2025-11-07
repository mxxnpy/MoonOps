# MoonOps

MoonOps is a scalable ASP.NET Core application built with Domain-Driven Design (DDD) principles, featuring OpenAPI documentation with Scalar UI.

## Project Structure

The solution follows a clean DDD architecture with clear separation of concerns:

```
MoonOps/
├── src/
│   ├── MoonOps.Api/               # API layer - HTTP endpoints and middleware
│   ├── MoonOps.Application/       # Application layer - business logic orchestration
│   │   ├── Models/                # Data models for data transfer between layers
│   │   ├── Interfaces/            # Application service interfaces
│   │   └── Services/              # Application services
│   ├── MoonOps.Domain/            # Domain layer - business entities and rules
│   │   ├── Entities/              # Domain entities
│   │   ├── Interfaces/            # Repository interfaces
│   │   └── ValueObjects/          # Value objects
│   └── MoonOps.Infrastructure/    # Infrastructure layer - external concerns
│       ├── Data/                  # Database context and configurations
│       └── Repositories/          # Repository implementations
└── tests/
    └── MoonOps.Tests/             # Unit and integration tests
```

## Features

- ✅ Domain-Driven Design (DDD) architecture
- ✅ Clean Architecture with dependency inversion
- ✅ OpenAPI/Swagger documentation with Scalar UI
- ✅ Health check endpoint
- ✅ Extensible repository pattern
- ✅ Ready for integration with central repository for integrators and shared code

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later

### Building the Solution

```bash
dotnet build
```

### Running the Application

```bash
cd src/MoonOps.Api
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

In development mode, you can access:
- **Scalar UI**: `https://localhost:5001/scalar/v1`
- **OpenAPI JSON**: `https://localhost:5001/openapi/v1.json`

### Running Tests

```bash
dotnet test
```

## API Endpoints

### Health Check
- **GET** `/health`
  - Returns the health status of the API
  - Response: `{ "status": "Healthy", "timestamp": "2025-11-07T..." }`

## Architecture Overview

### Domain Layer (`MoonOps.Domain`)
Contains business entities, value objects, and domain interfaces. This layer has no dependencies on other layers.

### Application Layer (`MoonOps.Application`)
Orchestrates business logic and coordinates between the domain and infrastructure layers. Contains Models and application services.

### Infrastructure Layer (`MoonOps.Infrastructure`)
Implements interfaces defined in the domain layer. Handles data persistence, external services, and other infrastructure concerns.

### API Layer (`MoonOps.Api`)
Exposes HTTP endpoints using minimal APIs. Configures middleware, dependency injection, and hosts the application.

## Scalability Considerations

The architecture is designed for scalability:

1. **Modular Design**: Each layer is a separate project, allowing independent scaling and deployment
2. **Repository Pattern**: Abstracts data access for easy replacement or multiple implementations
3. **DDD Principles**: Business logic is isolated in the domain layer
4. **Dependency Inversion**: High-level modules don't depend on low-level modules
5. **Central Repository Ready**: Structure supports integration with a central repository for shared integrators and code modules

## Next Steps

- Add Entity Framework Core for database persistence
- Implement authentication and authorization
- Add logging and monitoring
- Create domain-specific entities and services
- Integrate with central code repository
- Add API versioning
- Implement CQRS pattern if needed

## License

This project is licensed under the MIT License.