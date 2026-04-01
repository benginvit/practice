using OrderModel = Order.Domain.Order;

namespace Order.Infra.Db;

public class OrderReportService : Order.Domain.IOrderReportService
{
    public Task<List<OrderModel>> GetOrderHistoryAsync(int customerId)
    {
        // Simulate fetching order history from a database
        var orders = new List<OrderModel>
        {
            new OrderModel ( 1, customerId, "customer1@test.com", new List<string> { "Item1", "Item2" } ),
            new OrderModel ( 2, customerId, "customer2@test.com", new List<string> { "Item3", "Item4" } )
        };
        return Task.FromResult(orders);
    }
}
