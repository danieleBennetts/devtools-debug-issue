# C# Dev Kit — Go to Definition Repro

Minimal reproduction for [dotnet/vscode-csharp#8530](https://github.com/dotnet/vscode-csharp/issues/8530):
**"Go to Definition" navigates to decompiled metadata instead of source file for project references.**

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

1. Open `src/SampleApp.sln` in VS Code with C# Dev Kit
2. Open any test file (e.g., `OrderTests.cs`)
3. Place cursor on `CreateOrderRequest` or `CreateOrderResponse`
4. Press F12 (Go to Definition)
5. **Expected:** navigates to `SampleApp/Models/CreateOrderRequest.cs`
6. **Actual:** opens decompiled metadata file from `%LOCALAPPDATA%\Temp\MetadataAsSource\...`

## Running tests

```bash
# Unit tests
dotnet test src/SampleApp.UnitTests

# Integration tests (requires Docker)
dotnet test src/SampleApp.IntegrationTests
```
