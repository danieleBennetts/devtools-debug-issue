# C# Dev tools — Debug test issus

## Structure

```
src/
  SampleApp.sln
  SampleApp/                        # Class library (the target of Go to Definition)
    Models/
      CreateOrderRequest.cs
      CreateOrderResponse.cs
      Order.cs
    Persistence/
      AppDbContext.cs
    Services/
      IOrderService.cs
      OrderService.cs
  SampleApp.UnitTests/              # Unit tests (references SampleApp)
  SampleApp.IntegrationTests/       # Integration tests with SQL Server TestContainers
```

## Prerequisites

- .NET 10 SDK
- Docker (for integration tests)

## Steps to reproduce

1. Open `src/SampleApp.sln` in VS Code with C# Dev Tools
2. Open the test runner or codelens in a file, click debug.

## Running tests

```bash
# Unit tests
dotnet test src/SampleApp.UnitTests

# Integration tests (requires Docker)
dotnet test src/SampleApp.IntegrationTests
```
