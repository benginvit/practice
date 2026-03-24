using OrderModel = Order.Domain.Order;

namespace Order.Service.Events;

public record OrderPlacedEvent(OrderModel Order) : Order.Service.IEvent;
