using Application.Orders;

namespace Infrastructure;

public class OrderReportService : IOrderReportService
{
    public Task<List<Order>> GetOrderHistoryAsync(int customerId)
    {
        // Simulate fetching order history from a database
        var orders = new List<Order>
        {
            new Order ( 1, customerId, "customer1@test.com", new List<string> { "Item1", "Item2" } ),
            new Order ( 2, customerId, "customer2@test.com", new List<string> { "Item3", "Item4" } )
        };
        return Task.FromResult(orders);
    }
}