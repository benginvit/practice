using OrderModel = Order.Domain.Order;

namespace Order.Domain;

public interface IOrderReportService
{
    Task<List<OrderModel>> GetOrderHistoryAsync(int customerId);
}
