namespace Application.Orders;

public interface IOrderRepository
{
    Task<Order> GetOrderByIdAsync(int orderId);
    Task SaveOrderAsync(Order order);
}
