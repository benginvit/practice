using Order.Contracts;
using OrderModel = Order.Domain.Order;

namespace Order.Service;

public class OrderFacade : IOrderFacade
{
    private readonly IOrderService _orderService;
    private readonly IOrderReportService _orderReportService;

    public OrderFacade(IOrderService orderService, IOrderReportService orderReportService)
    {
        _orderService = orderService;
        _orderReportService = orderReportService;
    }

    public async Task<OrderModel> GetOrderByIdAsync(int orderId)
    {
        return await _orderService.GetOrderByIdAsync(orderId);
    }

    public async Task<PlaceOrderResult> PlaceOrderAsync(OrderModel order)
    {
        return await _orderService.PlaceOrderAsync(order);
    }

    public async Task<List<OrderModel>> GetOrderHistoryAsync(int customerId)
    {
        return await _orderReportService.GetOrderHistoryAsync(customerId);
    }

    public async Task<OrderSummary> GetOrderSummaryAsync(int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        var history = await _orderReportService.GetOrderHistoryAsync(order.CustomerId);
        return new OrderSummary(order, history);
    }
}
