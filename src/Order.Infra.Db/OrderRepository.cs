using OrderModel = Order.Domain.Order;

namespace Order.Infra.Db;

public class OrderRepository : Order.Service.IOrderRepository
{
    private static readonly List<OrderModel> _orders = [
        new OrderModel(1, 123, "customer@example.com", new List<string> { "Item1", "Item2" }),
        new OrderModel(2, 456, "customer2@example.com", new List<string> { "Item3", "Item4" }),
        new OrderModel(3, 789, "customer3@example.com", new List<string> { "Item5", "Item6" })
    ];
    public Task<OrderModel> GetOrderByIdAsync(int orderId)
    {
        // Simulate fetching order from a data source
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {orderId} not found.");
        }
        return Task.FromResult(order);
    }
    public Task SaveOrderAsync(OrderModel order)
    {
        _orders.Add(order);
        return Task.CompletedTask;
    }
}
