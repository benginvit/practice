using Order.Contracts;
using OrderModel = Order.Domain.Order;

namespace Order.Service;

public interface IOrderService
{
    Task<OrderModel> GetOrderByIdAsync(int orderId);
    Task<PlaceOrderResult> PlaceOrderAsync(OrderModel order);
}
