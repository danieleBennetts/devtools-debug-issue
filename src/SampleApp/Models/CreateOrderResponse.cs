namespace SampleApp.Models;

public sealed record CreateOrderResponse(
    Guid OrderId,
    string ProductName,
    int Quantity,
    decimal TotalPrice,
    DateTimeOffset CreatedAtUtc);
