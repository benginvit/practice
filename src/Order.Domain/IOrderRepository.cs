using OrderModel = Order.Domain.Order;

namespace Order.Domain;

public interface IOrderRepository
{
    Task<OrderModel> GetOrderByIdAsync(int orderId);
    Task SaveOrderAsync(OrderModel order);
}
