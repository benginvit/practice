namespace Order.Domain;

public interface IEventAggregator
{
    Task PublishAsync<T>(T @event) where T : IEvent;
}
