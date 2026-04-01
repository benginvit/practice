using Order.Domain;
using Order.Service.Events;
using Order.Service.Validation;
using OrderModel = Order.Domain.Order;

namespace Order.Service;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderValidationFactory _orderValidationFactory;
    private readonly IEventAggregator _eventAggregator;

    public OrderService(IOrderRepository orderRepository, IOrderValidationFactory orderValidationFactory, IEventAggregator eventAggregator)
    {
        _orderRepository = orderRepository;
        _orderValidationFactory = orderValidationFactory;
        _eventAggregator = eventAggregator;
    }
    public async Task<OrderModel> GetOrderByIdAsync(int orderId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(orderId);
        // Simulate fetching order from a data source
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        var orderValidation = _orderValidationFactory.Create(order.CustomerType);
        if (!orderValidation.ValidateOrder(order))
        {
            throw new InvalidOperationException("Order validation failed.");
        }
        return order;
    }
    public async Task<PlaceOrderResult> PlaceOrderAsync(OrderModel order)
    {
        var orderValidation = _orderValidationFactory.Create(order.CustomerType);

        if (!orderValidation.ValidateOrder(order))
        {
            return new PlaceOrderResult(false, "Order validation failed.");
        }
        var orderWithTimestamp = order with { CreatedAt = SystemTime.Now };
        await _orderRepository.SaveOrderAsync(orderWithTimestamp);
        await _eventAggregator.PublishAsync(new OrderPlacedEvent(orderWithTimestamp));
        return new PlaceOrderResult(true, "Order placed successfully.");
    }

}
