namespace Application.Orders;

public interface IOrderReportService{
    Task<List<Order>> GetOrderHistoryAsync(int customerId);
}
