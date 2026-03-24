using Order.Contracts;
using OrderModel = Order.Domain.Order;

namespace Order.Service;

public interface IOrderFacade
{
    Task<OrderModel> GetOrderByIdAsync(int orderId);
    Task<PlaceOrderResult> PlaceOrderAsync(OrderModel order);
    Task<List<OrderModel>> GetOrderHistoryAsync(int customerId);
    Task<OrderSummary> GetOrderSummaryAsync(int orderId);
}
