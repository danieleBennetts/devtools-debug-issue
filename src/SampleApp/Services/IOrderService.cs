using SampleApp.Models;

namespace SampleApp.Services;

public interface IOrderService
{
    ValueTask<CreateOrderResponse> CreateOrderAsync(
        CreateOrderRequest request,
        CancellationToken ct = default);

    ValueTask<Order?> GetOrderByIdAsync(
        Guid orderId,
        CancellationToken ct = default);
}
