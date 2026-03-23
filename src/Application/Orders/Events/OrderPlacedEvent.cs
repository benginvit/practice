using Application.Events;

namespace Application.Orders.Events;

public record OrderPlacedEvent(Order Order) : IEvent;
