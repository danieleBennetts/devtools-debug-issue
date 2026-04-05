namespace SampleApp.Models;

public sealed record CreateOrderRequest(
    Guid CustomerId,
    string ProductName,
    int Quantity,
    decimal UnitPrice);
