using OrderModel = Order.Domain.Order;

namespace Order.Domain;

public interface IOrderFacade
{
    Task<OrderModel> GetOrderByIdAsync(int orderId);
    Task<PlaceOrderResult> PlaceOrderAsync(OrderModel order);
    Task<List<OrderModel>> GetOrderHistoryAsync(int customerId);
    Task<OrderSummary> GetOrderSummaryAsync(int orderId);
}
