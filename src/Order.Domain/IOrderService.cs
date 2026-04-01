using OrderModel = Order.Domain.Order;

namespace Order.Domain;

public interface IOrderService
{
    Task<OrderModel> GetOrderByIdAsync(int orderId);
    Task<PlaceOrderResult> PlaceOrderAsync(OrderModel order);
}
