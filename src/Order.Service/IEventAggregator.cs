namespace Order.Service;

public interface IEventAggregator
{
    Task PublishAsync<T>(T @event) where T : IEvent;
}
