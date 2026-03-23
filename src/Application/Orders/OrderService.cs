using Application.Events;
using Application.Orders.Events;
using Application.Orders.Validation;

namespace Application.Orders;

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
    public async Task<Order> GetOrderByIdAsync(int orderId)
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
    public async Task<PlaceOrderResult> PlaceOrderAsync(Order order)
    {
        var orderValidation = _orderValidationFactory.Create(order.CustomerType);

        if (!orderValidation.ValidateOrder(order))
        {
            return new PlaceOrderResult(false, "Order validation failed.");
        }
        await _orderRepository.SaveOrderAsync(order);
        await _eventAggregator.PublishAsync(new OrderPlacedEvent(order));
        return new PlaceOrderResult(true, "Order placed successfully.");
    }

}
