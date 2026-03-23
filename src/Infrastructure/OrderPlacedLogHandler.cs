using Application.Events;
using Application.Orders.Events;

namespace Infrastructure;

public class OrderPlacedLogHandler : IEventHandler<OrderPlacedEvent>
{
    public Task HandleAsync(OrderPlacedEvent @event)
    {
        Console.WriteLine($"[LOG] Order {@event.Order.Id} placed for customer {@event.Order.CustomerId}");
        return Task.CompletedTask;
    }
}
