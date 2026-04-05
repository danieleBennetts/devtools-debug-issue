using Microsoft.EntityFrameworkCore;
using SampleApp.Models;
using SampleApp.Persistence;

namespace SampleApp.Services;

public sealed class OrderService(
    AppDbContext dbContext,
    TimeProvider timeProvider) : IOrderService
{
    public async ValueTask<CreateOrderResponse> CreateOrderAsync(
        CreateOrderRequest request,
        CancellationToken ct = default)
    {
        var order = Order.Create(request: request, timeProvider: timeProvider);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(ct);

        return order.ToResponse();
    }

    public async ValueTask<Order?> GetOrderByIdAsync(
        Guid orderId,
        CancellationToken ct = default)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId, ct);
    }
}
