namespace Application.Orders;

public interface IOrderService
{
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<PlaceOrderResult> PlaceOrderAsync(Order order);
}
