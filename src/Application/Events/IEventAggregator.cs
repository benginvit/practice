namespace Application.Events;

public interface IEventAggregator
{
    Task PublishAsync<T>(T @event) where T : IEvent;
}
