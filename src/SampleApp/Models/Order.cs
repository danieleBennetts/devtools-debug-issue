namespace SampleApp.Models;

public sealed class Order
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal TotalPrice { get; init; }
    public DateTimeOffset CreatedAtUtc { get; init; }

    public static Order Create(
        CreateOrderRequest request,
        TimeProvider timeProvider)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            ProductName = request.ProductName,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            TotalPrice = request.Quantity * request.UnitPrice,
            CreatedAtUtc = timeProvider.GetUtcNow(),
        };
    }

    public CreateOrderResponse ToResponse()
    {
        return new CreateOrderResponse(
            OrderId: Id,
            ProductName: ProductName,
            Quantity: Quantity,
            TotalPrice: TotalPrice,
            CreatedAtUtc: CreatedAtUtc);
    }
}
