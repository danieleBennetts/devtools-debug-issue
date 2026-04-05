using SampleApp.Models;

namespace SampleApp.UnitTests;

public class OrderTests
{
    [Fact]
    public void Create_Should_Calculate_TotalPrice()
    {
        // Arrange
        var timeProvider = new FakeTimeProvider(
            new DateTimeOffset(2025, 6, 15, 10, 0, 0, TimeSpan.Zero));

        var request = new CreateOrderRequest(
            CustomerId: Guid.NewGuid(),
            ProductName: "Widget",
            Quantity: 3,
            UnitPrice: 10.50m);

        // Act
        var order = Order.Create(request: request, timeProvider: timeProvider);

        // Assert
        order.TotalPrice.Should().Be(31.50m);
        order.ProductName.Should().Be("Widget");
        order.Quantity.Should().Be(3);
        order.CreatedAtUtc.Should().Be(timeProvider.GetUtcNow());
    }

    [Fact]
    public void Create_Should_Set_CustomerId()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var request = new CreateOrderRequest(
            CustomerId: customerId,
            ProductName: "Gadget",
            Quantity: 1,
            UnitPrice: 99.99m);

        // Act
        var order = Order.Create(request: request, timeProvider: TimeProvider.System);

        // Assert
        order.CustomerId.Should().Be(customerId);
    }

    [Fact]
    public void ToResponse_Should_Map_All_Fields()
    {
        // Arrange
        var now = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var timeProvider = new FakeTimeProvider(now);

        var request = new CreateOrderRequest(
            CustomerId: Guid.NewGuid(),
            ProductName: "Thingamajig",
            Quantity: 2,
            UnitPrice: 5.00m);

        var order = Order.Create(request: request, timeProvider: timeProvider);

        // Act
        var response = order.ToResponse();

        // Assert
        response.OrderId.Should().Be(order.Id);
        response.ProductName.Should().Be("Thingamajig");
        response.Quantity.Should().Be(2);
        response.TotalPrice.Should().Be(10.00m);
        response.CreatedAtUtc.Should().Be(now);
    }
}

/// <summary>
/// Minimal fake time provider for unit tests.
/// </summary>
public sealed class FakeTimeProvider(DateTimeOffset fixedUtcNow) : TimeProvider
{
    public override DateTimeOffset GetUtcNow() => fixedUtcNow;
}
