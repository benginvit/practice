using Microsoft.Extensions.DependencyInjection;

namespace Order.Infra;

public class EventAggregator : Order.Domain.IEventAggregator
{
    private readonly IServiceProvider _serviceProvider;

    public EventAggregator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<T>(T @event) where T : Order.Domain.IEvent
    {
        var handlers = _serviceProvider.GetServices<Order.Domain.IEventHandler<T>>();
        foreach (var handler in handlers)
            await handler.HandleAsync(@event);
    }
}
