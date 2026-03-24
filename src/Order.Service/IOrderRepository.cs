using OrderModel = Order.Domain.Order;

namespace Order.Service;

public interface IOrderRepository
{
    Task<OrderModel> GetOrderByIdAsync(int orderId);
    Task SaveOrderAsync(OrderModel order);
}
