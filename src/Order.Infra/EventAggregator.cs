using Microsoft.Extensions.DependencyInjection;

namespace Order.Infra;

public class EventAggregator : Order.Service.IEventAggregator
{
    private readonly IServiceProvider _serviceProvider;

    public EventAggregator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<T>(T @event) where T : Order.Service.IEvent
    {
        var handlers = _serviceProvider.GetServices<Order.Service.IEventHandler<T>>();
        foreach (var handler in handlers)
            await handler.HandleAsync(@event);
    }
}
