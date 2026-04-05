using Microsoft.EntityFrameworkCore;
using SampleApp.Models;
using SampleApp.Persistence;
using SampleApp.Services;

[assembly: AssemblyFixture(typeof(SampleApp.IntegrationTests.SqlServerFixture))]

namespace SampleApp.IntegrationTests;

public class OrderServiceIntegrationTests(SqlServerFixture sqlFixture) : IAsyncLifetime
{
    private AppDbContext _dbContext = null!;
    private OrderService _sut = null!;

    public async ValueTask InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(sqlFixture.ConnectionString)
            .Options;

        _dbContext = new AppDbContext(options);
        await _dbContext.Database.EnsureCreatedAsync();

        _sut = new OrderService(
            dbContext: _dbContext,
            timeProvider: TimeProvider.System);
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

    [Fact]
    public async Task Should_Persist_Order_To_Database()
    {
        // Arrange
        var request = new CreateOrderRequest(
            CustomerId: Guid.NewGuid(),
            ProductName: "Integration Test Widget",
            Quantity: 2,
            UnitPrice: 25.00m);

        // Act
        var response = await _sut.CreateOrderAsync(request);

        // Assert
        response.OrderId.Should().NotBeEmpty();
        response.TotalPrice.Should().Be(50.00m);

        var persisted = await _sut.GetOrderByIdAsync(response.OrderId);
        persisted.Should().NotBeNull();
        persisted!.ProductName.Should().Be("Integration Test Widget");
    }

    [Fact]
    public async Task Should_Return_Null_For_Nonexistent_Order()
    {
        // Act
        var result = await _sut.GetOrderByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Should_Map_Response_Correctly()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var request = new CreateOrderRequest(
            CustomerId: customerId,
            ProductName: "Mapped Product",
            Quantity: 5,
            UnitPrice: 10.00m);

        // Act
        var response = await _sut.CreateOrderAsync(request);

        // Assert
        response.ProductName.Should().Be("Mapped Product");
        response.Quantity.Should().Be(5);
        response.TotalPrice.Should().Be(50.00m);
    }
}
