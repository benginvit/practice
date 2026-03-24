using OrderModel = Order.Domain.Order;

namespace Order.Service;

public interface IOrderReportService
{
    Task<List<OrderModel>> GetOrderHistoryAsync(int customerId);
}
