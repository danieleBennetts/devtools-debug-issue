using SampleApp.Models;

namespace SampleApp.UnitTests;

public class CreateOrderRequestTests
{
    [Fact]
    public void Should_Create_Request_With_All_Properties()
    {
        // Arrange & Act
        var customerId = Guid.NewGuid();
        var request = new CreateOrderRequest(
            CustomerId: customerId,
            ProductName: "Test Product",
            Quantity: 5,
            UnitPrice: 19.99m);

        // Assert
        request.CustomerId.Should().Be(customerId);
        request.ProductName.Should().Be("Test Product");
        request.Quantity.Should().Be(5);
        request.UnitPrice.Should().Be(19.99m);
    }

    [Fact]
    public void Should_Support_Equality_By_Value()
    {
        var customerId = Guid.NewGuid();

        var request1 = new CreateOrderRequest(
            CustomerId: customerId,
            ProductName: "Widget",
            Quantity: 1,
            UnitPrice: 10m);

        var request2 = new CreateOrderRequest(
            CustomerId: customerId,
            ProductName: "Widget",
            Quantity: 1,
            UnitPrice: 10m);

        request1.Should().Be(request2);
    }
}
