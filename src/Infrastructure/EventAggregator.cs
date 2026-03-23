using Application.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class EventAggregator : IEventAggregator
{
    private readonly IServiceProvider _serviceProvider;

    public EventAggregator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<T>(T @event) where T : IEvent
    {
        var handlers = _serviceProvider.GetServices<IEventHandler<T>>();
        foreach (var handler in handlers)
            await handler.HandleAsync(@event);
    }
}
