# KingPrice

A modern, scalable solution built with .NET Aspire, Blazor Server, and Entity Framework Core.

## 📁 Solution Structure

The solution is organized into five distinct projects, each with a specific responsibility in the data and logic flow:

### KingPrice.AppHost (Orchestrator)
The central nervous system of the app. It uses .NET Aspire to manage service discovery, configure the SQL Server Docker container, and inject connection strings into the API and Web projects.

### KingPrice.Web (Presentation Layer)
A Blazor Server frontend utilizing Interactive Server Rendering. It provides a responsive UI for managing users and viewing system-wide statistics.

### KingPrice.Api (Interface Adapter Layer)
A RESTful Web API that handles HTTP requests, enforces business rules, and communicates with the data layer.

### KingPrice.Core (Data & Infrastructure Layer)
Contains the Entity Framework Core `DbContext`, database migrations, and the physical data models. It serves as the single source of truth for the system's state.

### KingPrice.Abstraction (Contract Layer)
A shared library containing DTOs (Data Transfer Objects). This project ensures that the `Web` and `Api` layers are synchronized without the frontend needing direct access to the database entities.

## 🏗️ Key Technical Decisions

### 1. .NET Aspire for Service Orchestration

We chose .NET Aspire over traditional Docker Compose setups to streamline the developer experience.

**Benefit:** Aspire handles the lifecycle of the SQL Server container and provides a built-in dashboard for monitoring logs, traces, and metrics across all services in real-time.

### 2. Decoupled Contract Pattern

The system strictly uses DTOs (e.g., `UserResponse`, `CreateUserRequest`) located in the Abstraction layer for all network communication.

**Decision:** We never expose raw Entity Framework models to the Blazor frontend.

**Benefit:** This prevents "Circular Dependency" issues and allows the database schema to evolve (e.g., changing table names or logic) without breaking the UI.

### 3. Interactive Server Rendering (Blazor)

The UI is configured for Interactive Server Mode in .NET 10.

**Decision:** This allows for server-side execution with real-time DOM updates via SignalR.

**Benefit:** We achieve a rich, stateful user experience (like modal dialogs and instant list refreshes) without the complexity of client-side state management or manual JavaScript interop.

### 4. Typed HttpClient with Dependency Injection

The frontend communicates with the API through a central `IUserApiClient` service.

**Decision:** API logic is abstracted away from the UI components.

**Benefit:** This makes the code highly testable and ensures that error handling (like API timeouts or 404s) is managed in one central location rather than duplicated across pages.
